
using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using marknote.Models;

namespace marknote.Services
{
    public class NoteSearch : INoteSearch, IDisposable
    {
        private readonly DirectoryInfo indexDirectoryInfo;
        private readonly Analyzer analyzer;
        private FSDirectory indexDirectory;
        private IndexReader indexReader;
        private IndexSearcher indexSearcher;
        private QueryParser queryParser;
        private bool warm;

        public NoteSearch(INotebook notebook)
        {
            string indexPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".marknote", "index");

            analyzer = new WhitespaceAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);

            indexDirectoryInfo = new DirectoryInfo(indexPath);

            // TODO - change all console access to log statements
            Console.WriteLine("Index path: {0}", indexPath);

            RebuildIndex(notebook.NoteDir, indexPath);
        }

        private void RebuildIndex(string notePath, string indexPath)
        {
            // TODO - make sure index is closed!

            if (System.IO.Directory.Exists(indexPath))
            {
                System.IO.Directory.Delete(indexPath, true);
            }

            using (var indexDir = FSDirectory.Open(indexDirectoryInfo))
            {
                var config = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);

                using (var indexWriter = new IndexWriter(indexDir, config))
                {
                    var noteDirInfo = new DirectoryInfo(notePath);

                    foreach (var file in noteDirInfo.GetFiles())
                    {
                        if (!file.Name.EndsWith(".md") && !file.Name.EndsWith(".txt"))
                        {
                            continue;
                        }

                        var doc = new Document();

                        doc.AddStringField("Name", file.Name, Field.Store.YES);
                        doc.AddTextField("Content", file.OpenText());

                        indexWriter.AddDocument(doc);
                    }
                }
            }
        }

        private void Warmup()
        {
            indexDirectory = FSDirectory.Open(indexDirectoryInfo);
            indexReader = DirectoryReader.Open(indexDirectory);
            indexSearcher = new IndexSearcher(indexReader);
            queryParser = new QueryParser(LuceneVersion.LUCENE_48, "Content", analyzer);

            warm = true;
        }

        public List<SearchResultModel> Search(string query)
        {
            if (!warm)
            {
                Warmup();
            }

            List<SearchResultModel> result = new List<SearchResultModel>();

            var parsed = queryParser.Parse(query);
            ScoreDoc[] hits = indexSearcher.Search(parsed, null, 40).ScoreDocs;

            for (int i = 0; i < hits.Length; i++)
            {
                var doc = indexSearcher.Doc(hits[i].Doc);

                var model = new SearchResultModel
                {
                    Name = doc.Get("Name")
                };

                result.Add(model);
            }

            // TODO
            // return string.Format("Query: '{0}', {1} hits", query, hits.Length);
            return result;
        }

        public void Dispose()
        {
            if (indexReader != null)
            {
                indexReader.Dispose();
                indexReader = null;
            }

            if (indexDirectory != null)
            {
                indexDirectory.Dispose();
                indexDirectory = null;
            }
        }
    }
}
