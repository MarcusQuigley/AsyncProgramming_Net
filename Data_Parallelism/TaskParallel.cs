using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Parallelism
{
    public partial class TaskParallel : Form
    {
        string theEBook;
        readonly Uri bookUri = new Uri("http://www.gutenberg.org/files/98/98-8.txt");
        public TaskParallel()
        {
            InitializeComponent();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            WebClient wc1 = new WebClient();
            wc1.DownloadStringCompleted += (s, args) =>
                {
                    theEBook = args.Result;
                    textBoxBook.Text = theEBook;

                };

            wc1.DownloadStringAsync(bookUri);
        }
 

        private void buttonGetStats_Click(object sender, EventArgs e)
        {
            string[] words = theEBook.Split(new char[] 
            { ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/' }
                ,StringSplitOptions.RemoveEmptyEntries);
            string longestWord = "";
            string[] tenMostCommonWords = null; 

Parallel.Invoke(() =>
    {
              longestWord = FindLongestWord(words);
    }, ()=>
            {
               tenMostCommonWords = FindTenMostCommonWords(words);
        });

            foreach (string command in tenMostCommonWords)
                listBox1.Items.Add(command);

            textBoxBook.Text = longestWord;


            

        }

        private string[] FindTenMostCommonWords(string[] words)
        {
            var commonWords = from word in words.AsParallel() where word.Length > 6
                               group word by word into g orderby g.Count() descending
                              select g.Key;
 


            string[] result =  commonWords.Take(10).ToArray();

            return result;

 
        }

        private string FindLongestWord(string[] words)
        {
           return (from word in words 
                          orderby word.Length descending select word).First();
          //  return result;
        }
    }
}
