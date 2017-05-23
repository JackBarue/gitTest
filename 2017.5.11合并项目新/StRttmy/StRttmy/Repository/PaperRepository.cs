using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Model;

namespace StRttmy.Repository
{
    public class PaperRepository : RepositoryBase<TestPaper>
    {
        public string sqlSelect = "";
        public string sqlOrderby = "";
        public string sqlWhere = "";

        public IList<TestPaper> TestPaperAll(DateTime star, DateTime end)
        {
            return dbContext.TestPapers.Where(r => r.CreateTime >= star && r.CreateTime <= end).ToList<TestPaper>();
        }

        public IList<StudentPaperListShowClass> StudentPaperPOP(DateTime star, DateTime end)
        {
            sqlSelect = string.Format(@"select * from(SELECT d.StudentExamPaperId,e.TestName,c.ClassName,a.Name,e.TestTime,d.TestState,ISNULL(SUM(h.Score),0) paperscore,ISNULL(SUM(i.AnswerScore),0) score FROM Students a
            LEFT JOIN ClassStudents b
            ON a.StudentId=b.StudentId
            LEFT JOIN StClasses c
            ON b.StClassId=c.StClassId
            LEFT JOIN StudentExamPapers d
            ON a.StudentId=d.StudentId
            LEFT JOIN TestPapers e
            ON d.TestPaperId=e.TestPaperId
            LEFT JOIN ExamPapers f
            ON e.TestPaperId=f.TestPaperId
            LEFT JOIN TestQuestions h
            ON f.TestQuestionId=h.TestQuestionId
            LEFT JOIN StudentAnswers i
            ON h.TestQuestionId=i.TestQuestionId AND d.StudentId=i.StudentId AND e.TestPaperId=i.TestPaperId            
            WHERE  d.TestState=3 AND (e.CreateTime BETWEEN'{0}' AND '{1}')
			GROUP BY d.StudentExamPaperId,e.TestName,c.ClassName,a.Name,e.TestTime,d.TestState
			)aa
			where aa.score>60", star, end);
            return dbContext.Database.SqlQuery<StudentPaperListShowClass>(sqlSelect).ToList();
        }

        public IList<StudentPaperListShowClass> StudentPaper(DateTime star, DateTime end)
        {
            sqlSelect = string.Format(@"SELECT d.StudentExamPaperId,e.TestName,c.ClassName,a.Name,e.TestTime,d.TestState,h.Score,i.AnswerScore FROM Students a
            LEFT JOIN ClassStudents b
            ON a.StudentId=b.StudentId
            LEFT JOIN StClasses c
            ON b.StClassId=c.StClassId
            LEFT JOIN StudentExamPapers d
            ON a.StudentId=d.StudentId
            LEFT JOIN TestPapers e
            ON d.TestPaperId=e.TestPaperId
            LEFT JOIN ExamPapers f
            ON e.TestPaperId=f.TestPaperId
            LEFT JOIN TestQuestions h
            ON f.TestQuestionId=h.TestQuestionId
            LEFT JOIN StudentAnswers i
            ON h.TestQuestionId=i.TestQuestionId AND d.StudentId=i.StudentId AND e.TestPaperId=i.TestPaperId            
            WHERE  d.TestState=2 or d.TestState=3 AND (e.CreateTime BETWEEN'{0}' AND '{1}') AND i.AnswerScore=0
			ORDER BY c.ClassName", star, end);
            return dbContext.Database.SqlQuery<StudentPaperListShowClass>(sqlSelect).ToList();
        }


        public Guid MatchingUser(Guid paperId)
        {
            var paperUserId = dbContext.TestPapers.SingleOrDefault(c => c.TestPaperId == paperId);
            Guid gid = paperUserId.UserId;
            return gid;
        }

        public IList<TestPaper> SearchPaperClass(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, Guid userid)
        {
            dbContext = new StRttmyContext();
            #region 选中系统项
            if (sysid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" OR StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
            }
            #endregion

            #region 选中工种项
            if (workid != Guid.Empty)
            {
                sqlWhere = "";
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
            }
            #endregion

            #region 选中科目项
            if (subjectid != Guid.Empty)
            {
                sqlWhere = "";
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StTypeId =N'{0}'", subjectid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeId =N'{0}'", subjectid);
                }
            }
            #endregion

            #region 选中类别项
            if (genreid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StTypeSupplyId=N'{0}'", genreid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeSupplyId=N'{0}'", genreid);
                }
            }
            #endregion

            #region 选中等级项
            if (levelid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StLevelId=N'{0}'", levelid);
                }
                else
                {
                    sqlWhere = string.Format(@" StLevelId=N'{0}'", levelid);
                }
            }
            #endregion

            #region 输入关键字
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND (TestName LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" TestName LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere + " AND ";
            }
            else
            {
                sqlWhere = " WHERE ";
            }
            sqlOrderby = " ORDER BY TestName";
            sqlSelect = string.Format(@"SELECT * FROM TestPapers{0} UserId='{1}'{2}", sqlWhere, userid, sqlOrderby);
            return dbContext.Database.SqlQuery<TestPaper>(sqlSelect).ToList();
        }


        /// <summary>
        /// 试卷按条件进行精确查找
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="sysid">系统</param>
        /// <param name="workid">工种</param>
        /// <param name="genreid">类别</param>
        /// <param name="levelid">等级</param>
        /// <param name="subjectid">科目</param>
        /// <returns>结果列表</returns>
        public IList<TestPaper> SearchPaper(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid)
        {
            dbContext = new StRttmyContext();
            #region 选中系统项
            if (sysid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" OR StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
            }
            #endregion

            #region 选中工种项
            if (workid != Guid.Empty)
            {
                sqlWhere = "";
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
            }
            #endregion

            #region 选中科目项
            if (subjectid != Guid.Empty)
            {
                sqlWhere = "";
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StTypeId =N'{0}'", subjectid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeId =N'{0}'", subjectid);
                }
            }
            #endregion

            #region 选中类别项
            if (genreid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StTypeSupplyId=N'{0}'", genreid);
                }
                else
                {
                    sqlWhere = string.Format(@" StTypeSupplyId=N'{0}'", genreid);
                }
            }
            #endregion

            #region 选中等级项
            if (levelid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND StLevelId=N'{0}'", levelid);
                }
                else
                {
                    sqlWhere = string.Format(@" StLevelId=N'{0}'", levelid);
                }
            }
            #endregion

            #region 输入关键字
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND (TestName LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" TestName LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            sqlOrderby = " ORDER BY TestName";
            sqlSelect = string.Format(@"SELECT * FROM TestPapers{0} {1}", sqlWhere, sqlOrderby);
            return dbContext.Database.SqlQuery<TestPaper>(sqlSelect).ToList();
        }

        /// <summary>
        /// 按试卷名称匹配试卷
        /// </summary>
        /// <param name="PaperName">试卷名称</param>
        /// <returns>查询结果集</returns>
        public TestPaper MatchingPaper(string PaperName, Guid PaperId)
        {
            return dbContext.TestPapers.SingleOrDefault(c => c.TestName == PaperName && c.TestPaperId != PaperId);
        }

        /// <summary>
        /// 新增试卷
        /// </summary>
        /// <param name="addquestion">试卷信息</param>
        /// <returns>新增结果</returns>
        public override bool Add(TestPaper addpaper)
        {
            if (addpaper == null)
            {
                return false;
            }
            dbContext.TestPapers.Add(addpaper);
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Update(TestPaper updatepaper)
        {
            using (dbContext = new StRttmyContext())
            {
                dbContext.TestPapers.Attach(updatepaper);
                dbContext.Entry<TestPaper>(updatepaper).State = System.Data.EntityState.Modified;
                if (dbContext.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckClassExamPaper(Guid paperId)
        {
            var classexampaper = dbContext.ClassExamPapers.Where(c => c.TestPaperId == paperId).ToList();
            if (classexampaper.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePaper(Guid paperId)
        {
            sqlSelect = string.Format(@"DELETE FROM ExamPapers WHERE TestPaperId=N'{0}'", paperId);
            dbContext.TestPapers.Remove(dbContext.TestPapers.SingleOrDefault(c => c.TestPaperId == paperId));
            if (dbContext.SaveChanges() > 0 || dbContext.Database.ExecuteSqlCommand(sqlSelect) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override TestPaper Find(Guid testpaperId)
        {
            return dbContext.TestPapers.SingleOrDefault(c => c.TestPaperId == testpaperId);
        }

        public ExamPaper MatchingExamPaper(Guid testpaperId, Guid testquestionId)
        {
            return dbContext.ExamPapers.SingleOrDefault(c => c.TestPaperId == testpaperId && c.TestQuestionId == testquestionId);
        }

        public List<ExamPaper> FindExamPaer(Guid paperid)
        {
            return dbContext.ExamPapers.Where(c => c.TestPaperId == paperid).ToList();
        }

        public bool DerivedExampaper(List<ExamPaper> newEP, Guid paperid)
        {
            if (newEP == null || paperid == Guid.Empty)
            {
                return false;
            }
            foreach (var item in newEP)
            {
                ExamPaper newExamPaper = new ExamPaper();
                newExamPaper.ExamPaperId = Guid.NewGuid();
                newExamPaper.TestPaperId = paperid;
                newExamPaper.TestQuestionId = item.TestQuestionId;
                dbContext.ExamPapers.Add(newExamPaper);
            }
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MSAddExamPaper(List<ExamPaper> listep)
        {
            if (listep == null)
            {
                return false;
            }
            foreach (var item in listep)
            {
                item.ExamPaperId = Guid.NewGuid();
                dbContext.ExamPapers.Add(item);
            }
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteExamQuestion(List<ExamPaper> listep)
        {
            if (listep == null)
            {
                return false;
            }

            foreach (var item in listep)
            {
                dbContext.ExamPapers.Remove(dbContext.ExamPapers.SingleOrDefault(c => c.TestQuestionId == item.TestQuestionId && c.TestPaperId == item.TestPaperId));
            }
            if (dbContext.SaveChanges() > 0)
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
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlSelect = string.Format(@"SELECT * FROM StClasses WHERE ClassName LIKE '%{0}%'", keyword);
            }
            else
            {
                sqlSelect = string.Format(@"SELECT * FROM StClasses");
            }

            return dbContext.Database.SqlQuery<StClass>(sqlSelect).ToList();
        }

        public IList<StClass> SearchOtherClass(string keyword, Guid testpaperid)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlWhere = string.Format(@" ClassName LIKE'%{0}%'", keyword);
            }
            if (testpaperid != Guid.Empty)
            {
                sqlWhere = string.Format(@"a.TestPaperId='{0}'", testpaperid);
            }
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = "WHERE " + sqlWhere;
            }
            sqlSelect = string.Format(@"SELECT * FROM StClasses WHERE StClassId NOT IN(
            SELECT a.StClassId FROM ClassExamPapers a
            LEFT JOIN StClasses b
            ON a.StClassId=b.StClassId {0}) AND ClassState=N'启用' ORDER BY ClassName", sqlWhere);
            return dbContext.Database.SqlQuery<StClass>(sqlSelect).ToList();
        }

        public List<TestQuestion> GetTestQuestions(Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int type, int num, double score)
        {
            List<TestQuestion> lst = new List<TestQuestion>();
            if (sysid != Guid.Empty)
            {
                if (type > 0)
                {
                    if (num > 0 && score > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE StTypeId=N'{1}' OR Fid=N'{1}'))) and QuestionType={2} and score={3} order by newid()", num, sysid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (num > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE StTypeId=N'{1}' OR Fid=N'{1}'))) and QuestionType={2} order by newid()", num, sysid, type);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (score > 0)
                    {
                        string sql = string.Format(@"select top (25) * from TestQuestions where StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}'))) and QuestionType={1} and score={2} order by newid()", sysid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                }
            }
            if (workid != Guid.Empty)
            {
                if (type > 0)
                {
                    if (num > 0 && score > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{1}')) and QuestionType={2} and score={3} order by newid()", num, workid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (num > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{1}')) and QuestionType={2} order by newid()", num, workid, type);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (score > 0)
                    {
                        string sql = string.Format(@"select top (25) * from TestQuestions where StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}')) and QuestionType={1} and score={2} order by newid()", workid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                }
            }

            if (genreid != Guid.Empty)
            {
                if (type > 0)
                {
                    if (num > 0 && score > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeSupplyId=N'{1}' and QuestionType={2} and score={3} order by newid()", num, genreid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (num > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeSupplyId=N'{1}' and QuestionType={2} order by newid()", num, genreid, type);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (score > 0)
                    {
                        string sql = string.Format(@"select top (25) * from TestQuestions where StTypeSupplyId=N'{0}' and QuestionType={1} and score={2} order by newid()", genreid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                }
            }

            if (levelid != Guid.Empty)
            {
                if (type > 0)
                {
                    if (num > 0 && score > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StLevelId=N'{1}' and QuestionType={2} and score={3} order by newid()", num, levelid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (num > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StLevelId=N'{1}' and QuestionType={2} order by newid()", num, levelid, type);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (score > 0)
                    {
                        string sql = string.Format(@"select top (25) * from TestQuestions where StLevelId=N'{0}' and QuestionType={1} and score={2} order by newid()", levelid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                }
            }

            if (subjectid != Guid.Empty)
            {
                if (type > 0)
                {
                    if (num > 0 && score > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeId =N'{1}' and QuestionType={2} and score={3} order by newid()", num, subjectid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (num > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where StTypeId =N'{1}' and QuestionType={2} order by newid()", num, subjectid, type);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (score > 0)
                    {
                        string sql = string.Format(@"select top (25) * from TestQuestions where StTypeId =N'{0}' and QuestionType={1} and score={2} order by newid()", subjectid, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                }
            }
            else
            {
                if (type > 0)
                {
                    if (num > 0 && score > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where QuestionType={1} and score={2} order by newid()", num, type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (num > 0)
                    {
                        string sql = string.Format(@"select top {0} * from TestQuestions where QuestionType={1} order by newid()", num, type);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                    else if (score > 0)
                    {
                        string sql = string.Format(@"select top (25) * from TestQuestions where QuestionType={0} and score={1} order by newid()", type, score);
                        lst = dbContext.Database.SqlQuery<TestQuestion>(sql).ToList();
                    }
                }
            }
            return lst;
        }

        public bool ATAddExamPaper(List<TestQuestion> listtq, Guid paperId)
        {
            if (listtq == null)
            {
                return false;
            }
            else
            {
                int testquestionnum = 0;
                foreach (var item in listtq)
                {
                    ExamPaper addep = new ExamPaper();
                    if (Convert.ToInt32(item.QuestionType) == 1)
                    {
                        testquestionnum = testquestionnum + 1;
                    }
                    if (Convert.ToInt32(item.QuestionType) == 2)
                    {
                        testquestionnum = testquestionnum + 1;
                    }
                    if (Convert.ToInt32(item.QuestionType) == 3)
                    {
                        testquestionnum = testquestionnum + 1;
                    }
                    if (Convert.ToInt32(item.QuestionType) == 4)
                    {
                        testquestionnum = testquestionnum + 1;
                    }
                    addep.ExamPaperId = Guid.NewGuid();
                    addep.TestQuestionId = item.TestQuestionId;
                    addep.TestPaperId = paperId;
                    addep.TestQuestionNo = testquestionnum;
                    dbContext.ExamPapers.Add(addep);
                }
                if (dbContext.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddClassExamPaper(List<ClassExamPaper> addcep)
        {
            if (addcep == null)
            {
                return false;
            }
            foreach (var item in addcep)
            {
                ClassExamPaper addclasspaper = new ClassExamPaper();
                IQueryable<ClassExamPaper> existcep = dbContext.ClassExamPapers.Where(c => c.TestPaperId == item.TestPaperId && c.StClassId == item.StClassId);
                if (existcep.Count() == 0)
                {
                    addclasspaper.ClassExamPaperId = Guid.NewGuid();
                    addclasspaper.StClassId = item.StClassId;
                    addclasspaper.TestPaperId = item.TestPaperId;
                    dbContext.ClassExamPapers.Add(addclasspaper);
                    dbContext.SaveChanges();
                }

                sqlSelect = string.Format(@"select NEWID() as StudentExamPaperId,b.StudentId,c.TestPaperId,0 as TestState from StClasses a
                left join ClassStudents b
                on a.StClassId=b.StClassId
                left join ClassExamPapers c
                on c.StClassId=b.StClassId
                where a.StClassId='{0}'", addclasspaper.StClassId);
                var studentId = dbContext.Database.SqlQuery<StudentExamPaper>(sqlSelect).ToList();
                if (studentId.Count() == 0)
                {
                    return false;
                }
                else
                {
                    foreach (var stu in studentId)
                    {
                        IQueryable<StudentExamPaper> existsep = dbContext.StudentExamPapers.Where(c => c.StudentId == stu.StudentId && c.TestPaperId == stu.TestPaperId);
                        if (existsep.Count() == 0)
                        {
                            StudentExamPaper addstupaper = new StudentExamPaper();
                            addstupaper.StudentExamPaperId = Guid.NewGuid();
                            addstupaper.StudentId = stu.StudentId;
                            addstupaper.TestPaperId = addclasspaper.TestPaperId;
                            addstupaper.TestState = TestStateType.未考;
                            dbContext.StudentExamPapers.Add(addstupaper);
                        }
                    }
                }
            }
            if (dbContext.SaveChanges() > 0)
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
            IList<StudentPaperListShowClass> findpaperList = null;

            #region 选中系统项
            if (sysid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" OR e.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
                else
                {
                    sqlWhere = string.Format(@" e.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE Fid IN(
                     SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
            }
            #endregion

            #region 选中工种项
            if (workid != Guid.Empty)
            {
                sqlWhere = "";
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND e.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
                else
                {
                    sqlWhere = string.Format(@" e.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
            }
            #endregion

            #region 选中科目项
            if (subjectid != Guid.Empty)
            {
                sqlWhere = "";
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND e.StTypeId =N'{0}'", subjectid);
                }
                else
                {
                    sqlWhere = string.Format(@" e.StTypeId =N'{0}'", subjectid);
                }
            }
            #endregion

            #region 选中类别项
            if (genreid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND e.StTypeSupplyId=N'{0}'", genreid);
                }
                else
                {
                    sqlWhere = string.Format(@" e.StTypeSupplyId=N'{0}'", genreid);
                }
            }
            #endregion

            #region 选中等级项
            if (levelid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND e.StLevelId=N'{0}'", levelid);
                }
                else
                {
                    sqlWhere = string.Format(@" e.StLevelId=N'{0}'", levelid);
                }
            }
            #endregion

            #region 考试状态
            if (teststate != 0)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND a.TestState ={0}", teststate);
                }
                else
                {
                    sqlWhere = string.Format(@" a.TestState ={0}", teststate);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere + " AND ";
            }
            else
            {
                sqlWhere = " WHERE ";
            }
            sqlOrderby = " ORDER BY a.TestName";
            string sqlGroupby = "GROUP BY a.StudentExamPaperId,a.TestName,c.ClassName,a.Name,a.TestTime,a.TestState,a.paperscore,a.score";
            sqlSelect = string.Format(@"select a.StudentExamPaperId,a.TestName,c.ClassName,a.Name,a.TestTime,a.TestState,a.paperscore,a.score from 
			(select b.StudentExamPaperId,a.StudentId,e.TestPaperId,a.Name,e.TestName,e.TestTime,b.TestState,ISNULL(SUM(h.Score),0) paperscore,ISNULL(SUM(i.AnswerScore),0) score from Students a
			LEFT JOIN StudentExamPapers b
            ON a.StudentId=b.StudentId
			LEFT JOIN TestPapers e
            ON b.TestPaperId=e.TestPaperId
            LEFT JOIN ExamPapers f
            ON e.TestPaperId=f.TestPaperId
            LEFT JOIN TestQuestions h
            ON f.TestQuestionId=h.TestQuestionId
            LEFT JOIN StudentAnswers i
            ON h.TestQuestionId=i.TestQuestionId AND a.StudentId=i.StudentId AND e.TestPaperId=i.TestPaperId 
			{0} a.StudentId='{1}'
			group by a.Name,e.TestTime,b.TestState,a.StudentId,e.TestPaperId,b.StudentExamPaperId,e.TestName) as a
			left join ClassExamPapers b
			on a.TestPaperId=b.TestPaperId
			left join StClasses c
			on b.StClassId=c.StClassId {2} {3}", sqlWhere, userid, sqlGroupby, sqlOrderby);
            findpaperList = dbContext.Database.SqlQuery<StudentPaperListShowClass>(sqlSelect).ToList();
            foreach (var item in findpaperList)
            {
                if (item.StudentExamPaperId == null)
                {
                    findpaperList = null;
                }
            }
            return findpaperList;
        }

        public bool AddStudentAnswer(List<StudentAnswer> listsa, Guid paperid, Guid studentid)
        {
            if (listsa == null)
            {
                return false;
            }
            else
            {
                foreach (var item in listsa)
                {
                    StudentAnswer newstudentanswer = new StudentAnswer();
                    newstudentanswer.AnswerId = Guid.NewGuid();
                    newstudentanswer.TestPaperId = item.TestPaperId;
                    newstudentanswer.TestQuestionId = item.TestQuestionId;
                    newstudentanswer.StudentId = item.StudentId;
                    newstudentanswer.Answer = item.Answer;
                    newstudentanswer.AnswerScore = item.AnswerScore;
                    dbContext.StudentsAnswers.Add(newstudentanswer);
                }
            }
            if (dbContext.SaveChanges() > 0)
            {
                string sql = string.Format(@"UPDATE StudentExamPapers SET TestState=2 WHERE StudentId='{0}' AND TestPaperId='{1}'", studentid, paperid);
                if (dbContext.Database.ExecuteSqlCommand(sql) > 0)
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

        public IList<ReadPapersClass> ReadStudentPaper(string keyword, Guid userid)
        {
            IList<ReadPapersClass> studentpaper = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlWhere = string.Format(@"WHERE a.Name LIKE '%{0}%' OR c.ClassName LIKE '%{0}%' OR a.TestName LIKE '%{0}%'", keyword);
            }

            sqlSelect = string.Format(@"select a.StudentExamPaperId,a.TestName,c.ClassName,a.Name,a.TestState from (
			select d.StudentExamPaperId,e.TestName,a.StudentId,a.Name,d.TestState,c.StClassId from students a
			LEFT JOIN StudentExamPapers d
            ON a.StudentId=d.StudentId
            LEFT JOIN TestPapers e
            ON d.TestPaperId=e.TestPaperId
			left join ClassExamPapers c
			on d.TestPaperId=c.TestPaperId
			WHERE e.UserId='{0}' AND d.TestState!=1) as a
			LEFT JOIN ClassStudents b
            ON a.StClassId=b.StClassId
            LEFT JOIN StClasses c
            ON b.StClassId=c.StClassId {1} ORDER BY c.ClassName", userid, sqlWhere);
            studentpaper = dbContext.Database.SqlQuery<ReadPapersClass>(sqlSelect).ToList();
            return studentpaper;
        }

        public StudentExamPaper MatchingStuPaper(Guid studentexampaperId)
        {
            return dbContext.StudentExamPapers.SingleOrDefault(c => c.StudentExamPaperId == studentexampaperId);
        }

        public StudentAnswer FindStuAnswer(Guid stuId, Guid questionId, Guid paperid)
        {
            return dbContext.StudentsAnswers.SingleOrDefault(c => c.StudentId == stuId && c.TestQuestionId == questionId && c.TestPaperId == paperid);
        }

        public IList<StudentAnswer> FindStuTestScore(Guid stuId, Guid paperid)
        {
            return dbContext.StudentsAnswers.Where(c => c.StudentId == stuId && c.TestPaperId == paperid).ToList();
        }

        public bool UpdateScore(List<StudentAnswer> scorelist, Guid paperid, Guid studentid)
        {
            if (scorelist == null)
            {
                return false;
            }
            else
            {
                foreach (var item in scorelist)
                {
                    string update = string.Format(@"UPDATE StudentAnswers
                    SET AnswerScore={0}
                    WHERE StudentId='{1}' AND TestQuestionId='{2}' AND TestPaperId='{3}'", item.AnswerScore, item.StudentId, item.TestQuestionId, item.TestPaperId);
                    dbContext.Database.ExecuteSqlCommand(update);
                }
                string sql = string.Format(@"UPDATE StudentExamPapers SET TestState=3 WHERE StudentId='{0}' AND TestPaperId='{1}'", studentid, paperid);
                if (dbContext.Database.ExecuteSqlCommand(sql) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IList<ExaminationPaperClass> ExaminationPaper(string keyword)
        {
            IList<ExaminationPaperClass> studentpaper = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlWhere = string.Format(@" WHERE a.Name like '%{0}%' or a.TestName like '%{0}%' or a.ClassName like '%{0}%'", keyword);
            }
            sqlSelect = string.Format(@"select a.StudentExamPaperId,a.ClassName,a.Name,a.TestName,a.TestTime,a.TestState,ISNULL(SUM(g.Score),0) paperscore,ISNULL(SUM(i.AnswerScore),0) score from (
            select d.StudentExamPaperId,a.TestquestionId,e.StudentId,f.TestPaperId,h.ClassName,e.Name,f.TestName,f.TestTime,d.TestState from Exampapers a
            right join ClassExamPapers b
            on a.TestPaperId=b.TestPaperId
            left join classStudents c
            on b.StClassId=c.StClassId
            left join StClasses h
            on b.StClassId=h.StClassId
            left join StudentExamPapers d
            on c.StudentId=d.StudentId and a.TestPaperId=d.TestPaperId
            left join Students e
            on c.StudentId=e.StudentId
            left join TestPapers f
            on b.TestPaperId=f.TestPaperId) as a
            left join TestQuestions g
            on a.TestquestionId=g.TestquestionId
            left join StudentAnswers i
            on g.TestQuestionId=i.TestQuestionId AND a.StudentId=i.StudentId AND a.TestPaperId=i.TestPaperId  {0}
            group by a.StudentExamPaperId,a.ClassName,a.Name,a.TestTime,a.TestState,a.TestName
			ORDER BY a.ClassName", sqlWhere);
            studentpaper = dbContext.Database.SqlQuery<ExaminationPaperClass>(sqlSelect).ToList();
            return studentpaper;
        }

        public StudentExamPaper SearchState(Guid studentexampaperid)
        {
            return dbContext.StudentExamPapers.SingleOrDefault(c => c.StudentExamPaperId == studentexampaperid);
        }

        public bool CheckTestQuestionResources(Guid courseId)
        {
            sqlSelect = string.Format(@"select  a.* from TestQuestions a
                left join TestQuestionResources b
                on a.TestQuestionId=b.TestQuestionId
                left join Resources c
                on b.ResourceId=c.ResourceId
                left join CoursewareResources d
                on c.Title=d.name
                left join Coursewares e
                on d.CoursewareId=e.CoursewareId
                where e.CoursewareId='{0}'", courseId);
            var tqr = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            if (tqr.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddCoursewarePapers(Guid paperId, Guid courseId)
        {
            if (courseId == Guid.Empty || paperId == Guid.Empty)
            {
                return false;
            }

            List<TestQuestion> questionList = new List<TestQuestion>();
            sqlSelect = string.Format(@"select top(40) a.* from TestQuestions a
                left join TestQuestionResources b
                on a.TestQuestionId=b.TestQuestionId
                left join Resources c
                on b.ResourceId=c.ResourceId
                left join CoursewareResources d
                on c.Title=d.name
                left join Coursewares e
                on d.CoursewareId=e.CoursewareId
                where e.CoursewareId=N'{0}'
                order by NEWID()", courseId);
            questionList = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            if (questionList.Count() == 0)
            {
                return false;
            }
            foreach (var item in questionList)
            {
                ExamPaper ep = new ExamPaper();
                ep.ExamPaperId = Guid.NewGuid();
                ep.TestPaperId = paperId;
                ep.TestQuestionId = item.TestQuestionId;
                dbContext.ExamPapers.Add(ep);
            }
            if (dbContext.SaveChanges() > 0)
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
