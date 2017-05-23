using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Model;
using StRttmy.Repository;

namespace StRttmy.BLL
{
    public class QuestionBLL
    {
        private QuestionRepository questionRsy;

        public Guid MatchingUser(Guid questionId)
        {
            questionRsy = new QuestionRepository();
            Guid gid = questionRsy.MatchingUser(questionId);
            return gid;
        }

        public int testquestioncount(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid)
        {
            questionRsy = new QuestionRepository();
            int findquestionNum = questionRsy.testquestioncount(keyword, sysid, workid, genreid, levelid, subjectid);
            return findquestionNum;
        }

        public int resourcequestioncount(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, Guid resourceid)
        {
            questionRsy = new QuestionRepository();
            int findquestionNum = questionRsy.resourcequestioncount(keyword, sysid, workid, genreid, levelid, subjectid, resourceid);
            return findquestionNum;
        }

        public int paperquestioncount(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, Guid paperid)
        {
            questionRsy = new QuestionRepository();
            int findquestionNum = questionRsy.paperquestioncount(keyword, sysid, workid, genreid, levelid, subjectid, paperid);
            return findquestionNum;
        }

        public IList<TestQuestion> SearchQuestions(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent)
        {
            questionRsy = new QuestionRepository();
            IList<TestQuestion> findquestionList = questionRsy.SearchQuestions(keyword, sysid, workid, genreid, levelid, subjectid, pageSize, pageCurrent);
            return findquestionList;
        }

        public IList<TestQuestion> SearchQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid)
        {
            questionRsy = new QuestionRepository();
            IList<TestQuestion> findquestionList = questionRsy.SearchQuestion(keyword, sysid, workid, genreid, levelid, subjectid);
            return findquestionList;
        }

        public IList<TestQuestion> SearchPaperQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int questiontype, Guid testpaperId)
        {
            questionRsy = new QuestionRepository();
            IList<TestQuestion> findquestionList = questionRsy.SearchPaperQuestion(keyword, sysid, workid, genreid, levelid, subjectid, questiontype, testpaperId);
            return findquestionList;
        }

        public IList<TestQuestion> NewSearchResourceQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent, Guid resourceId)
        {
            questionRsy = new QuestionRepository();
            IList<TestQuestion> findquestionList = questionRsy.NewSearchResourceQuestion(keyword, sysid, workid, genreid, levelid, subjectid, pageSize, pageCurrent, resourceId);
            return findquestionList;
        }

        public IList<TestQuestion> NewSearchPaperQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent, Guid testpaperId)
        {
            questionRsy = new QuestionRepository();
            IList<TestQuestion> findquestionList = questionRsy.NewSearchPaperQuestion(keyword, sysid, workid, genreid, levelid, subjectid, pageSize, pageCurrent, testpaperId);
            return findquestionList;
        }

        public TestQuestion MatchingQuestion(string questionName,Guid questionId)
        {
            questionRsy = new QuestionRepository();
            TestQuestion testqution = questionRsy.MatchingQuestion(questionName, questionId);
            return testqution;
        }

        public bool AddQuestion(TestQuestion addquestion)
        {
            questionRsy = new QuestionRepository();
            if (questionRsy.Add(addquestion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateQuestion(TestQuestion updatequestion)
        {
            questionRsy = new QuestionRepository();
            var oldquestion = questionRsy.Find(updatequestion.TestQuestionId);
            if (oldquestion == null)
            {
                return false;
            }
            else
            {
                if (questionRsy.Update(updatequestion))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteQuestion(Guid testquestionId, out TestQuestion testquestion)
        {
            questionRsy = new QuestionRepository();
            TestQuestion question = null;
            if (questionRsy.Delete(testquestionId, out question))
            {
                testquestion = question;
                return true;
            }
            else
            {
                testquestion = null;
                return false;
            }
        }

        public bool DeleteQuestionList(List<TestQuestion> tqList)
        {
            if (tqList == null)
            {
                return false;
            }
            questionRsy = new QuestionRepository();
            if (questionRsy.DeleteQuestionList(tqList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TestQuestion GetQuestion(Guid testquestionId)
        {
            questionRsy = new QuestionRepository();
            TestQuestion testquestion = questionRsy.Find(testquestionId);
            return testquestion;
        }

        public IList<TestQuestion> QuestioninPaperList(Guid testpaperId,int questiontype)
        {
            questionRsy = new QuestionRepository();
            IList<TestQuestion> findquestionList = questionRsy.QuestioninPaperList(testpaperId, questiontype).ToList();
            return findquestionList;
        }

        public int TestTime(Guid paperId)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.TestTime(paperId);
        }

        public IList<TestQuestionClass> Question(Guid TestpaperId)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.Question(TestpaperId);
        }

        public TestQuestion Find(Guid testquestionId)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.Find(testquestionId);
        }

        public bool FindQuestion(Guid testquestionId)
        {
            questionRsy = new QuestionRepository();
            if (questionRsy.FindQuestion(testquestionId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TestStateType TestState(Guid testpaperId, Guid studentId)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.TestState(testpaperId, studentId);
        }

        public bool AddQuestionResource(List<TestQuestionResource> Listtqr)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.AddQuestionResource(Listtqr);
        }

        public bool UpdateQuestionResource(List<TestQuestionResource> Listtqr, Guid testquestionId,Guid resourceId)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.UpdateQuestionResource(Listtqr, testquestionId, resourceId);
        }

        public IList<Resource> otherResource(Guid testquestionId, string keyword, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.otherResource(testquestionId, keyword,category,stlevel,subjects,typeofwork,systemtype);
        }

        public IList<Resource> ShowBindingResource(Guid testquestionId, string keyword, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.ShowBindingResource(testquestionId, keyword, category, stlevel, subjects, typeofwork, systemtype);
        }

        public IList<Resource> BindingResource(Guid testquestionId)
        {
            questionRsy = new QuestionRepository();
            return questionRsy.BindingResource(testquestionId);
        }
    }
}
