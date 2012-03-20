using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LateBindingApi.Core;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using NetOffice.VBIDEApi.Enums;

namespace wdump.Library
{
    public interface IWordDocument
    {
        void Open(string file);
        List<string> Read();
        Word.Range Content { get; }
        Paragraphs Paragraphs { get; }
        void Finally();
    }

 
    public class WordDocument : IWordDocument
    {
        Word.Application application;
        Word.Document document;

        public Word.Range Content 
        {
            get
            {
                Word.Range result = null;

                if (document != null)
                {
                    result = document.Content;
                }

                return result;
            }
        }

        public Paragraphs Paragraphs
        {
            get 
            {
                var result = new Paragraphs();
                var items = new List<Paragraph>();

                if (document != null)
                {
                    foreach (Word.Paragraph p in document.Paragraphs)
                    {
                        var item = new Paragraph();

                        item.Text = p.Range.Text;
                        item.Font = p.Range.Font.Name;
                        item.FontSize = p.Range.Font.Size.ToString();

                        item.Bold =  toBool(p.Range.Bold);
                        item.Italic =  toBool(p.Range.Italic);

                        items.Add(item);
                    }
                }

                result.Items = items;

                return result;
            }   
        }

        ~WordDocument()
        {
            Finally();
        }

        public void Open(string file)
        {
            try
            {
                initializeWord();
                document = application.Documents.Open(file);
            }
            catch (Exception ex)
            {
                Finally();
                throw ex;
            }
        }

        public void Finally()
        {
            if (document != null)
            {
                document.Close();
                document.Dispose();
                document = null;
            }
            if (application != null)
            {
                application.Quit();
                application.Dispose();
                application = null;
            }
        }

        public List<string> Read()
        {
            var result = new List<string>();

            foreach (Word.Paragraph p in document.Paragraphs)
            {
                result.Add(p.Range.Text);
            }

            return result;
        }

        private void initializeWord()
        {
            LateBindingApi.Core.Factory.Initialize();
            application = new Word.Application();
            application.DisplayAlerts = WdAlertLevel.wdAlertsNone;
        }

        private bool toBool(int value)
        {
            var result = false;

            if (value != 0)
                result = true;

            return result;
        }
    }
}
