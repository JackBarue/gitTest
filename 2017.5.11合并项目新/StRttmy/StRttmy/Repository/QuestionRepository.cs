using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    public class QuestionRepository : RepositoryBase<TestQuestion>
    {
        public string sqlSelect = "";
        public string sqlOrderby = "";
        public string sqlWhere = "";

        public IList<TestQuestionUserClass> TestQuestionAll(DateTime star, DateTime end)
        {
            sqlSelect = string.Format(@"select a.*,b.UserName from testquestions a
            left join users b
            on a.userid=b.userid
            where a.createtime between '{0}' and '{1}'",star,end);
            return dbContext.Database.SqlQuery<TestQuestionUserClass>(sqlSelect).ToList();
        }

        public Guid MatchingUser(Guid questionId)
        {
            var questionUserId = dbContext.TestQuestions.SingleOrDefault(c => c.TestQuestionId == questionId);
            Guid gid = questionUserId.UserId;
            return gid;
        }

        public int testquestioncount(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid)
        {
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
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            sqlOrderby = " ORDER BY CreateTime DESC";
            sqlSelect = string.Format(@"SELECT * FROM TestQuestions{0}{1}", sqlWhere, sqlOrderby);
            int findquestionNum = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).Count();
            return findquestionNum;
        }

        public int resourcequestioncount(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, Guid resourceid)
        {
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
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = sqlWhere + string.Format(@" AND TestQuestionId NOT IN (SELECT TestQuestionId FROM TestQuestionResources WHERE ResourceId=N'{0}')", resourceid);
            }
            else
            {
                sqlWhere = string.Format(@" TestQuestionId NOT IN (SELECT TestQuestionId FROM TestQuestionResources WHERE ResourceId=N'{0}')", resourceid);
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            sqlOrderby = " ORDER BY CreateTime DESC";
            sqlSelect = string.Format(@"SELECT * FROM TestQuestions{0}{1}", sqlWhere, sqlOrderby);
            int findquestionNum = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).Count();
            return findquestionNum;
        }

        public int paperquestioncount(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid,Guid paperid)
        {
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
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = sqlWhere + string.Format(@" AND TestQuestionId NOT IN (SELECT TestQuestionId FROM ExamPapers WHERE TestPaperId=N'{0}')", paperid);
            }
            else
            {
                sqlWhere = string.Format(@" TestQuestionId NOT IN (SELECT TestQuestionId FROM ExamPapers WHERE TestPaperId=N'{0}')", paperid);
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            sqlOrderby = " ORDER BY CreateTime DESC";
            sqlSelect = string.Format(@"SELECT * FROM TestQuestions{0}{1}", sqlWhere, sqlOrderby);
            int findquestionNum = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).Count();
            return findquestionNum;
        }

        public IList<TestQuestion> SearchQuestions(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent)
        {
            IList<TestQuestion> findquestionList = null;
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
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            string sqlWherenum = string.Format(@" WHERE number>{0}*({1}-1)", pageSize,pageCurrent);
            sqlSelect = string.Format(@"SELECT TOP {0} number,* FROM (SELECT ROW_NUMBER() OVER (ORDER BY CreateTime DESC) AS number, * FROM TestQuestions {1}) AS NewTestQuestions {2}" ,pageSize, sqlWhere,sqlWherenum);
            findquestionList = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            return findquestionList;
        }

        /// <summary>
        /// 试题按条件进行精确查找
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="sysid">系统</param>
        /// <param name="workid">工种</param>
        /// <param name="genreid">类别</param>
        /// <param name="levelid">等级</param>
        /// <param name="subjectid">科目</param>
        /// <returns>结果列表</returns>
        public IList<TestQuestion> SearchQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid)
        {
            IList<TestQuestion> findquestionList = null;
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
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            sqlOrderby = " ORDER BY CreateTime DESC";
            sqlSelect = string.Format(@"SELECT * FROM TestQuestions{0}{1}", sqlWhere, sqlOrderby);
            findquestionList = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            return findquestionList;
        }

        /// <summary>
        /// 匹配试题
        /// </summary>
        /// <param name="questionName">试题</param>
        /// <returns>查询结果集</returns>
        public TestQuestion MatchingQuestion(string questionName, Guid questionId)
        {
            TestQuestion testqution = dbContext.TestQuestions.SingleOrDefault(c => c.Question == questionName && c.TestQuestionId != questionId);
            return testqution;
        }

        /// <summary>
        /// 新增试题
        /// </summary>
        /// <param name="addquestion">试题信息</param>
        /// <returns>新增结果</returns>
        public override bool Add(TestQuestion addquestion)
        {
            if (addquestion == null)
            {
                return false;
            }
            dbContext.TestQuestions.Add(addquestion);
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新试题
        /// </summary>
        /// <param name="updatequestion">试题信息</param>
        /// <returns>更新结果</returns>
        public override bool Update(TestQuestion updatequestion)
        {
            using (dbContext = new StRttmyContext())
            {
                dbContext.TestQuestions.Attach(updatequestion);
                dbContext.Entry<TestQuestion>(updatequestion).State = System.Data.EntityState.Modified;
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

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="testquestionId">试题Id</param>
        /// <returns>SQL影响行数</returns>
        public bool Delete(Guid testquestionId, out TestQuestion testquestion)
        {
            TestQuestion questions = Find(testquestionId);
            IQueryable<ExamPaper> exampaper = dbContext.ExamPapers.Where(c => c.TestQuestionId == questions.TestQuestionId);
            if (exampaper.Count() > 0)
            {
                testquestion = null;
                return false;
            }
            else
            {
                dbContext.TestQuestions.Remove(dbContext.TestQuestions.SingleOrDefault(c => c.TestQuestionId == testquestionId));
                if (dbContext.SaveChanges() > 0)
                {
                    testquestion = questions;
                    return true;
                }
                else
                {
                    testquestion = null;
                    return false;
                }
            }
        }

        public bool DeleteQuestionList(List<TestQuestion> tqList)
        {
            foreach (var item in tqList)
            {
                dbContext.TestQuestions.Remove(dbContext.TestQuestions.SingleOrDefault(c => c.TestQuestionId == item.TestQuestionId));
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

        /// <summary>
        /// 按试题Id查找试题
        /// </summary>
        /// <param name="testquestionId">试题Id</param>
        /// <returns>查找结果</returns>
        public override TestQuestion Find(Guid testquestionId)
        {
            return dbContext.TestQuestions.SingleOrDefault(c => c.TestQuestionId == testquestionId);
        }

        public bool FindQuestion(Guid testquestionId)
        {
            var tqcount= dbContext.ExamPapers.Where(c => c.TestQuestionId == testquestionId).ToList();
            if (tqcount.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<TestQuestion> SearchPaperQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int questiontype, Guid testpaperId)
        {
            IList<TestQuestion> findquestionList = null;
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
            #region 选择试题类型
            if (questiontype!=0)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND QuestionType={0}", questiontype);
                }
                else
                {
                    sqlWhere = string.Format(@" QuestionType={0}", questiontype);
                }
            }
            #endregion
            #region 输入关键字
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%'", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = sqlWhere + string.Format(@" AND TestQuestionId NOT IN (SELECT TestQuestionId FROM ExamPapers WHERE TestPaperId=N'{0}')", testpaperId);
            }
            else
            {
                sqlWhere = string.Format(@" TestQuestionId NOT IN (SELECT TestQuestionId FROM ExamPapers WHERE TestPaperId=N'{0}')", testpaperId);
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            sqlOrderby = " ORDER BY CreateTime DESC";
            sqlSelect = string.Format(@"SELECT * FROM TestQuestions{0}{1}", sqlWhere, sqlOrderby);
            findquestionList = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            return findquestionList;
        }

        public IList<TestQuestion> NewSearchResourceQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent, Guid resourceId)
        {
            IList<TestQuestion> findquestionList = null;
            #region 选中系统项
            if (sysid != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" OR a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                            SELECT StTypeId FROM StTypes WHERE Fid IN(
                            SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", sysid);
                }
                else
                {
                    sqlWhere = string.Format(@" a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
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
                    sqlWhere = sqlWhere + string.Format(@" AND a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                            SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", workid);
                }
                else
                {
                    sqlWhere = string.Format(@" a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
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
                    sqlWhere = sqlWhere + string.Format(@" AND a.StTypeId =N'{0}'", subjectid);
                }
                else
                {
                    sqlWhere = string.Format(@" a.StTypeId =N'{0}'", subjectid);
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
                    sqlWhere = sqlWhere + string.Format(@" AND f.StLevelId=N'{0}'", levelid);
                }
                else
                {
                    sqlWhere = string.Format(@" f.StLevelId=N'{0}'", levelid);
                }
            }
            #endregion
            #region 输入关键字
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" AND (a.Question LIKE N'%{0}%' OR a.AnswerA LIKE N'%{0}%' OR a.AnswerB LIKE N'%{0}%' OR a.AnswerC LIKE N'%{0}%' OR a.AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" (a.Question LIKE N'%{0}%' OR a.AnswerA LIKE N'%{0}%' OR a.AnswerB LIKE N'%{0}%' OR a.AnswerC LIKE N'%{0}%' OR a.AnswerD LIKE N'%{0}%')", keyword);
                }
            }
            #endregion            

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            string sqlWherenum = string.Format(@" WHERE No>{0}*({1}-1)", pageSize, pageCurrent);

            sqlSelect = string.Format(@"SELECT TOP {0} * FROM (SELECT ROW_NUMBER() OVER(ORDER BY a.No) AS               No,a.TestQuestionId,a.Question,a.QuestionType,a.StTypeId,a.StTypeSupplyId,a.StLevelId,a.ResourceId,a.UserId,a.Score,a.AnswerA,a.AnswerB,a.AnswerC,a.AnswerD,a.Correct,a.CreateTime
			FROM (SELECT 1 AS No, a.TestQuestionId,a.Question,a.QuestionType,a.StTypeId,a.StTypeSupplyId,a.StLevelId,b.ResourceId,a.UserId,a.Score,a.AnswerA,a.AnswerB,a.AnswerC,a.AnswerD,a.Correct,a.CreateTime FROM TestQuestions a
			LEFT JOIN TestQuestionResources b
			ON a.TestQuestionId=b.TestQuestionId
			WHERE b.ResourceId IN (SELECT ResourceId FROM Resources WHERE ResourceId=N'{1}')UNION
			SELECT 2 AS No, * FROM TestQuestions 
			WHERE TestQuestionId NOT IN (SELECT TestQuestionId FROM TestQuestionResources WHERE ResourceId=N'{1}')) AS a
			LEFT JOIN StTypes b
            ON a.StTypeId=b.StTypeId
            LEFT JOIN StTypes c
            ON c.StTypeId=b.Fid
            LEFT JOIN StTypes d
            ON d.StTypeId=c.Fid
            LEFT JOIN StTypeSupplies e
            ON a.StTypeSupplyId=e.StTypeSupplyId
            LEFT JOIN StLevels f
            ON a.StLevelId=f.StLevelId {2}) AS NewTestQuestions {3}", pageSize,resourceId, sqlWhere, sqlWherenum);
            findquestionList = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            return findquestionList;
        }

        public IList<TestQuestion> NewSearchPaperQuestion(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid,int pageSize, int pageCurrent, Guid testpaperId)
        {
            IList<TestQuestion> findquestionList = null;
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
                    sqlWhere = sqlWhere + string.Format(@" AND (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" (Question LIKE N'%{0}%' OR AnswerA LIKE N'%{0}%' OR AnswerB LIKE N'%{0}%' OR AnswerC LIKE N'%{0}%' OR AnswerD LIKE N'%{0}%')", keyword);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = sqlWhere + string.Format(@" AND TestQuestionId NOT IN (SELECT TestQuestionId FROM ExamPapers WHERE TestPaperId=N'{0}')", testpaperId);
            }
            else
            {
                sqlWhere = string.Format(@" TestQuestionId NOT IN (SELECT TestQuestionId FROM ExamPapers WHERE TestPaperId=N'{0}')", testpaperId);
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE " + sqlWhere;
            }
            string sqlWherenum = string.Format(@" WHERE number>{0}*({1}-1)", pageSize, pageCurrent);
            sqlSelect = string.Format(@"SELECT TOP {0} number,* FROM (SELECT ROW_NUMBER() OVER (ORDER BY CreateTime DESC) AS number, * FROM TestQuestions {1}) AS NewTestQuestions {2}", pageSize, sqlWhere, sqlWherenum);
            findquestionList = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            return findquestionList;
        }

        public IList<TestQuestion> QuestioninPaperList(Guid testpaperId, int questiontype)
        {
            IList<TestQuestion> testquestion = null;
            if (questiontype!=0)
            {
                if (string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = string.Format(@" AND QuestionType={0}", questiontype);
                }
            }
            sqlSelect = string.Format(@"SELECT testquestions.* FROM TestQuestions testquestions
            LEFT JOIN ExamPapers exampapers
            ON testquestions.TestQuestionId=exampapers.TestQuestionId
            LEFT JOIN TestPapers testpapers
            ON exampapers.TestPaperId=testpapers.TestPaperId
            WHERE exampapers.TestPaperId='{0}'{1} GROUP BY testquestions.TestQuestionId,testquestions.AnswerA,testquestions.AnswerB,
			testquestions.AnswerC,testquestions.AnswerD,testquestions.Correct,testquestions.CreateTime,
			testquestions.Question,testquestions.QuestionType,testquestions.ResourceId,testquestions.Score,testquestions.StLevelId,
			testquestions.StTypeId,testquestions.StTypeSupplyId,testquestions.UserId ORDER BY testquestions.QuestionType", testpaperId, sqlWhere);
            testquestion = dbContext.Database.SqlQuery<TestQuestion>(sqlSelect).ToList();
            return testquestion;
        }

        public IList<TestQuestionClass> Question(Guid TestpaperId)
        {
            IList<TestQuestionClass> question = null;
            sqlSelect = string.Format(@"SELECT a.TestQuestionId,a.Question,a.QuestionType,a.AnswerA,a.AnswerB,a.AnswerC,a.AnswerD,a.Score,a.Correct FROM TestQuestions a
            LEFT JOIN ExamPapers b
            ON a.testquestionid=b.TestQuestionId
            LEFT JOIN TestPapers c
            ON b.TestPaperId=c.TestPaperId
            WHERE c.TestPaperId=N'{0}'
            GROUP BY a.TestQuestionId,a.Question,a.QuestionType,a.AnswerA,a.AnswerB,a.AnswerC,a.AnswerD,a.Score,a.Correct
            ORDER BY a.QuestionType", TestpaperId);
            question = dbContext.Database.SqlQuery<TestQuestionClass>(sqlSelect).ToList();
            return question;
        }

        public int TestTime(Guid paperId)
        {
            int TestTime = 0;
            var time = dbContext.TestPapers.SingleOrDefault(c => c.TestPaperId == paperId);
            TestTime = time.TestTime;
            return TestTime;
        }

        public TestStateType TestState(Guid testpaperId,Guid studentId)
        {
            var state = dbContext.StudentExamPapers.SingleOrDefault(c => c.TestPaperId == testpaperId && c.StudentId == studentId);
            return state.TestState;
        }

        public bool AddQuestionResource(List<TestQuestionResource> Listtqr)
        {
            if (Listtqr == null)
            {
                return false;
            }
            else
            {                
                foreach (var item in Listtqr)
                {
                    TestQuestionResource addresource = new TestQuestionResource();
                    addresource.QuestionResourceId = item.QuestionResourceId;
                    addresource.TestQuestionId = item.TestQuestionId;
                    addresource.ResourceId = item.ResourceId;
                    dbContext.TestQuestionResources.Add(addresource);
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

        public bool UpdateQuestionResource(List<TestQuestionResource> Listtqr,Guid testquestionId,Guid resourceId)
        {
            if (Listtqr == null)
            {
                return false;
            }
            else
            {
                if (testquestionId != Guid.Empty)
                {
                    List<TestQuestionResource> resourceold = new List<TestQuestionResource>();
                    resourceold = dbContext.TestQuestionResources.Where(c => c.TestQuestionId == testquestionId).ToList();
                    List<TestQuestionResource> deloldresource = resourceold.Except(Listtqr).ToList();
                    if (deloldresource.Count() > 0)
                    {
                        foreach (var item in deloldresource)
                        {
                            dbContext.TestQuestionResources.Remove(dbContext.TestQuestionResources.SingleOrDefault(c => c.QuestionResourceId == item.QuestionResourceId));
                        }
                    }
                    List<TestQuestionResource> addnewresource = Listtqr.Except(resourceold).ToList();
                    if (addnewresource.Count() > 0)
                    {
                        foreach (var item in addnewresource)
                        {
                            TestQuestionResource addresource = new TestQuestionResource();
                            addresource.QuestionResourceId = item.QuestionResourceId;
                            addresource.TestQuestionId = item.TestQuestionId;
                            addresource.ResourceId = item.ResourceId;
                            dbContext.TestQuestionResources.Add(addresource);
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
                else if (resourceId != Guid.Empty)
                {
                    List<TestQuestionResource> resourceold = new List<TestQuestionResource>();
                    resourceold = dbContext.TestQuestionResources.Where(c => c.ResourceId == resourceId).ToList();
                    List<TestQuestionResource> deloldresource = resourceold.Except(Listtqr).ToList();
                    if (deloldresource.Count() > 0)
                    {
                        foreach (var item in deloldresource)
                        {
                            dbContext.TestQuestionResources.Remove(dbContext.TestQuestionResources.SingleOrDefault(c => c.QuestionResourceId == item.QuestionResourceId));
                        }
                    }
                    List<TestQuestionResource> addnewresource = Listtqr.Except(resourceold).ToList();
                    if (addnewresource.Count() > 0)
                    {
                        foreach (var item in addnewresource)
                        {
                            TestQuestionResource addresource = new TestQuestionResource();
                            addresource.QuestionResourceId = item.QuestionResourceId;
                            addresource.TestQuestionId = item.TestQuestionId;
                            addresource.ResourceId = item.ResourceId;
                            dbContext.TestQuestionResources.Add(addresource);
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
                else
                {
                    return false;
                }
            }
        }

        public IList<Resource> otherResource(Guid testquestionId, string keyword, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (systemtype != Guid.Empty)
            {
                sqlWhere = string.Format(@"and a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", systemtype);
            }

            if (typeofwork != Guid.Empty)
            {
                sqlWhere = string.Format(@"and a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", typeofwork);
            }

            if (subjects != Guid.Empty)
            {
                sqlWhere = string.Format(@"and a.StTypeId =N'{0}'", subjects);
            }

            if (category != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@"and a.StTypeSupplyId=N'{0}'", category);
                }
                else
                {
                    sqlWhere = string.Format(@" a.StTypeSupplyId=N'{0}'", category);
                }
            }
            if (stlevel != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" and a.StLevelId=N'{0}'", stlevel);
                }
                else
                {
                    sqlWhere = string.Format(@" a.StLevelId=N'{0}'", stlevel);
                }
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" and (a.Keyword like N'%{0}%' or a.Title like N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" and (a.Keyword like N'%{0}%' or a.Title like N'%{0}%')", keyword);
                }
            }
            sqlSelect=string.Format(@"select * from Resources a
            left join StTypes b
            on a.StTypeId=b.StTypeId
            left join StTypes c
            on c.StTypeId=b.Fid
            left join StTypes d
            on d.StTypeId=c.Fid
            left join StTypeSupplies e
            on a.StTypeSupplyId=e.StTypeSupplyId
            left join StLevels f
            on a.StLevelId=f.StLevelId
            where a.ResourceId not in (select ResourceId from TestQuestionResources where TestQuestionId=N'{0}' {1})", testquestionId,sqlWhere);
            return dbContext.Database.SqlQuery<Resource>(sqlSelect).ToList();
        }

        public IList<Resource> ShowBindingResource(Guid testquestionId, string keyword, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (systemtype != Guid.Empty)
            {
                sqlWhere = string.Format(@"where a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", systemtype);
            }

            if (typeofwork != Guid.Empty)
            {
                sqlWhere = string.Format(@"where a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", typeofwork);
            }

            if (subjects != Guid.Empty)
            {
                sqlWhere = string.Format(@"where a.StTypeId =N'{0}'", subjects);
            }

            if (category != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@"and a.StTypeSupplyId=N'{0}'", category);
                }
                else
                {
                    sqlWhere = string.Format(@"where a.StTypeSupplyId=N'{0}'", category);
                }
            }
            if (stlevel != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" and a.StLevelId=N'{0}'", stlevel);
                }
                else
                {
                    sqlWhere = string.Format(@"where a.StLevelId=N'{0}'", stlevel);
                }
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = sqlWhere + string.Format(@" and (a.Keyword like N'%{0}%' or a.Title like N'%{0}%')", keyword);
                }
                else
                {
                    sqlWhere = string.Format(@" where a.Keyword like N'%{0}%' or a.Title like N'%{0}%'", keyword);
                }
            }
            sqlSelect = string.Format(@"select a.ResourceId,a.Level,a.SoundFile,a.Title,a.Keyword,a.TextFile,a.ContentFile,a.StTypeId,a.StTypeSupplyId,a.StLevelId,a.UserId,a.CreateTime 
            from (select 1 as No, * from Resources where ResourceId in (select ResourceId from TestQuestionResources where TestQuestionId=N'{0}')union
            select 2 as No,* from Resources where ResourceId not in (select ResourceId from TestQuestionResources where TestQuestionId=N'{0}')) as a 
            left join StTypes b
            on a.StTypeId=b.StTypeId
            left join StTypes c
            on c.StTypeId=b.Fid
            left join StTypes d
            on d.StTypeId=c.Fid
            left join StTypeSupplies e
            on a.StTypeSupplyId=e.StTypeSupplyId
            left join StLevels f
            on a.StLevelId=f.StLevelId {1}
            order by a.No", testquestionId, sqlWhere);
            return dbContext.Database.SqlQuery<Resource>(sqlSelect).ToList();
        }


        public IList<Resource> BindingResource(Guid testquestionId)
        {
            sqlSelect=string.Format(@"select * from Resources where ResourceId in (select ResourceId from TestQuestionResources where TestQuestionId=N'{0}')",testquestionId);
            return dbContext.Database.SqlQuery<Resource>(sqlSelect).ToList();
        }
    }
}
