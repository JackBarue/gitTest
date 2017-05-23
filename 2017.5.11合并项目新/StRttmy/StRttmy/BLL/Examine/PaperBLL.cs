using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Model;
using StRttmy.Repository;

namespace StRttmy.BLL
{
    public class PaperBLL
    {
        private PaperRepository paperRsy;

        public Guid MatchingUser(Guid paperId)
        {
            paperRsy = new PaperRepository();
            Guid gid = paperRsy.MatchingUser(paperId);
            return gid;
        }

        public IList<TestPaper> SearchPaperClass(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, Guid userid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.SearchPaperClass(keyword, sysid, workid, genreid, levelid, subjectid, userid);
        }

        public IList<TestPaper> SearchPaper(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.SearchPaper(keyword, sysid, workid, genreid, levelid, subjectid);
        }

        public TestPaper MatchingPaper(string paperName,Guid paperId)
        {
            paperRsy = new PaperRepository();
            return paperRsy.MatchingPaper(paperName,paperId);
        }

        public bool AddPaper(TestPaper addpaper)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.Add(addpaper))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckClassExamPaper(Guid paperId)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.CheckClassExamPaper(paperId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePaper(TestPaper updatepaper)
        {
            paperRsy = new PaperRepository();
            var oldpaper = paperRsy.Find(updatepaper.TestPaperId);
            if (oldpaper == null)
            {
                return false;
            }
            else
            {
                if (paperRsy.Update(updatepaper))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeletePaper(Guid paperId)
        {
            paperRsy = new PaperRepository();
            if (paperId != Guid.Empty)
            {
                if (paperRsy.DeletePaper(paperId))
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return false;
            }
        }

        public ExamPaper MatchingExamPaper(Guid testpaperId, Guid testquestionId)
        {
            return paperRsy.MatchingExamPaper(testpaperId, testquestionId);
        }

        public List<ExamPaper> FindExamPaer(Guid paperid)
        {
            paperRsy=new PaperRepository();
            return paperRsy.FindExamPaer(paperid);
        }

        public TestPaper GetPaper(Guid testpaperId)
        {
            paperRsy = new PaperRepository();
            return paperRsy.Find(testpaperId);
        }

        public bool DerivedExampaper(List<ExamPaper> newEP, Guid paperid)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.DerivedExampaper(newEP, paperid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MSAddExamPaper(List<ExamPaper> addep)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.MSAddExamPaper(addep))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteExamQuestion(List<ExamPaper> delep)
        {
            paperRsy = new PaperRepository();
            if(paperRsy.DeleteExamQuestion(delep))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<StClass> SearchClass(string keyword)
        {
            paperRsy = new PaperRepository();
            return paperRsy.SearchClass(keyword);
        }

        public IList<StClass> SearchOtherClass(string keyword, Guid testpaperid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.SearchOtherClass(keyword, testpaperid);
        }

        public bool AddClassExampaper(List<ClassExamPaper> addcep)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.AddClassExamPaper(addcep))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TestQuestion> GetTestQuestions(Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int questType, int qusetNumber, double questSorc)
        {
            paperRsy = new PaperRepository();
            return paperRsy.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, questType, qusetNumber, questSorc);
        }

        public bool ATAddExamPaper(List<TestQuestion> listtq, Guid paperId)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.ATAddExamPaper(listtq, paperId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<StudentPaperListShowClass> SearchStudentPaper(Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, Guid userid, int teststate)
        {
            paperRsy = new PaperRepository();
            return paperRsy.SearchStudentPaper(sysid, workid, genreid, levelid, subjectid, userid,teststate);
        }

        public bool AddStudentAnswer(List<StudentAnswer> listsa,Guid paperid,Guid studentid)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.AddStudentAnswer(listsa, paperid, studentid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<ReadPapersClass> ReadStudentPaper(string keyword, Guid userid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.ReadStudentPaper(keyword, userid);
        }

        public StudentExamPaper MatchingStuPaper(Guid studentexampaperId)
        {
            paperRsy = new PaperRepository();
            return paperRsy.MatchingStuPaper(studentexampaperId);
        }

        public StudentAnswer FindStuAnswer(Guid stuId, Guid questionId,Guid paperid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.FindStuAnswer(stuId, questionId,paperid);            
        }

        public IList<StudentAnswer> FindStuTestScore(Guid stuId, Guid paperid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.FindStuTestScore(stuId, paperid);   
        }

        public bool UpdateScore(List<StudentAnswer> scorelist, Guid paperid, Guid studentid)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.UpdateScore(scorelist, paperid, studentid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<ExaminationPaperClass> ExaminationPaper(string keyword)
        {
            paperRsy = new PaperRepository();
            return paperRsy.ExaminationPaper(keyword);
        }

        public StudentExamPaper SearchState(Guid studentexampaperid)
        {
            paperRsy = new PaperRepository();
            return paperRsy.SearchState(studentexampaperid);
        }

        public bool CheckTestQuestionResources(Guid courseId)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.CheckTestQuestionResources(courseId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddCoursewarePapers(Guid paperId,Guid courseId)
        {
            paperRsy = new PaperRepository();
            if (paperRsy.AddCoursewarePapers(paperId, courseId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
