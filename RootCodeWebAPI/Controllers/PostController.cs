using RootCodeWebAPI.Models;
using RootCodeWebAPI.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RootCodeWebAPI.Controllers
{
    [RoutePrefix("Api/post")]
    public class PostController : ApiController
    {
        RootCodeTestDBEntities DB = new RootCodeTestDBEntities();

        [Route("WriteAPost")]
        [HttpPost]
        public object writeAPost(Post post)
        {
            try
            {

                Post postObj = new Post();
                if (postObj.postId == 0)
                {
                    postObj.UserId = post.UserId;
                    postObj.PostText = post.PostText;
                    postObj.CreatedDateTime = DateTime.Now;
                    DB.Posts.Add(postObj);
                    findVowelCount(post.PostText);
                    DB.SaveChanges();
                    return new Response
                    { Status = "Success", Message = "Record SuccessFully Saved." };
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return new Response
            { Status = "Error", Message = "Invalid Data." };
        }

        private void findVowelCount(string postText)
        {
            int vowel_count = 0;
            int paired_vowel_count = 0;
            int wordCount = getWordCount(postText);
            for (int i = 0; i < postText.Length; i++)
            {
                switch (postText[i])
                {
                    case 'a':
                        vowel_count++;
                        paired_vowel_count = checkPairedVowel(i, postText, paired_vowel_count);
                        break;
                    case 'e':
                        vowel_count++;
                        paired_vowel_count = checkPairedVowel(i, postText, paired_vowel_count);
                        break;
                    case 'i':
                        vowel_count++;
                        paired_vowel_count = checkPairedVowel(i, postText, paired_vowel_count);
                        break;
                    case 'o':
                        vowel_count++;
                        paired_vowel_count = checkPairedVowel(i, postText, paired_vowel_count);
                        break;
                    case 'u':
                        vowel_count++;
                        paired_vowel_count = checkPairedVowel(i, postText, paired_vowel_count);
                        break;
                }
            }

            StatVowel statVowelLog = DB.StatVowels.Where(x => x.Date== DateTime.Today).FirstOrDefault();
            if (statVowelLog == null)
            {
                StatVowel statVowel = new StatVowel();
                statVowel.SingleVowelCount = vowel_count;
                statVowel.PairVowelCount = paired_vowel_count;
                statVowel.TotalWordCount = wordCount;
                statVowel.Date = DateTime.Today;
                DB.StatVowels.Add(statVowel);
            }
            else
            {
                statVowelLog.SingleVowelCount = statVowelLog.SingleVowelCount+ vowel_count;
                statVowelLog.PairVowelCount = statVowelLog.PairVowelCount + paired_vowel_count;
                statVowelLog.TotalWordCount = statVowelLog.TotalWordCount + wordCount;
            }
            
            DB.SaveChanges();
        }

        private int getWordCount(string str)
        {
            int i, wrd, l;
            l = 0;
            wrd = 1;
            while (l <= str.Length - 1)
            {
                if (str[l] == ' ' || str[l] == '\n' || str[l] == '\t')
                {
                    wrd++;
                }

                l++;
            }
            return wrd;
        }

        private int checkPairedVowel(int i, string postText, int paired_vowel_count)
        {
            if (i + 1 < postText.Length)
            {
                if (countPairedVowel(postText[i + 1]))
                    return ++paired_vowel_count;
            }
            return paired_vowel_count;
        }

        private bool countPairedVowel(char v)
        {
            switch (v)
            {
                case 'a':
                    return true;
                case 'e':
                    return true;
                case 'i':
                    return true;
                case 'o':
                    return true;
                case 'u':
                    return true;
            }
            return false;
        }

    }
}