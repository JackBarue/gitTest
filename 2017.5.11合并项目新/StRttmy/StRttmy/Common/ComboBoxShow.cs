using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Common
{
    public class ComboBoxShow
    {
        protected StRttmyContext dbContext;
        /// <summary>
        /// 系统、工种、科目ComboBox显示方法
        /// </summary>
        /// <param name="gid">父键</param>
        /// <returns>结果列表</returns>
        public List<StType> ThreeTypecmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> typeList = new List<StType>();
            var allList = dbContext.StTypes.AsEnumerable().Where(c => c.Fid == gid).OrderBy(c => c.Name).ToList();
            if (allList.Count() > 0)
            {
                StType firstItem = new StType();
                firstItem.Name = "全部";
                firstItem.StTypeId = Guid.Empty;
                typeList.Insert(0, firstItem);
                //typeList.Add(firstItem);
                foreach (var syslistItem in allList)
                {
                    StType allItem = new StType();
                    allItem.Name = syslistItem.Name;
                    allItem.StTypeId = syslistItem.StTypeId;
                    typeList.Add(allItem);
                }
                return typeList;
            }
            else
            {
                StType firstItem = new StType();
                firstItem.Name = "全部";
                firstItem.StTypeId = Guid.Empty;
                typeList.Add(firstItem);
            }
            return typeList;
        }

        /// <summary>
        /// 类别ComboBox显示方法
        /// </summary>
        /// <returns>类别结果列表</returns>
        public List<StTypeSupply> GenrecmbShow()
        {
            dbContext = new StRttmyContext();
            List<StTypeSupply> typeList = new List<StTypeSupply>();
            var genreList = dbContext.StTypeSupplies.AsEnumerable().OrderBy(c => c.StTypeName).ToList();
            if (genreList.Count() > 0)
            {
                StTypeSupply firstItem = new StTypeSupply();
                firstItem.StTypeName = "全部";
                firstItem.StTypeSupplyId = Guid.Empty;
                typeList.Add(firstItem);
                foreach (var syslistItem in genreList)
                {
                    StTypeSupply allItem = new StTypeSupply();
                    allItem.StTypeName = syslistItem.StTypeName;
                    allItem.StTypeSupplyId = syslistItem.StTypeSupplyId;
                    typeList.Add(allItem);
                }
                return typeList;
            }
            else
            {
                StTypeSupply firstItem = new StTypeSupply();
                firstItem.StTypeName = "全部";
                firstItem.StTypeSupplyId = Guid.Empty;
                typeList.Add(firstItem);
            }
            return typeList;
        }

        /// <summary>
        /// 等级ComboBox显示方法
        /// </summary>
        /// <returns>等级结果列表</returns>
        public List<StLevel> LevelcmbShow()
        {
            dbContext = new StRttmyContext();
            List<StLevel> typeList = new List<StLevel>();
            var LevelList = dbContext.StLevels.AsEnumerable().OrderBy(c => c.StLevelName).ToList();
            if (LevelList.Count() > 0)
            {
                StLevel firstItem = new StLevel();
                firstItem.StLevelName = "全部";
                firstItem.StLevelId = Guid.Empty;
                typeList.Add(firstItem);
                foreach (var syslistItem in LevelList)
                {
                    StLevel allItem = new StLevel();
                    allItem.StLevelName = syslistItem.StLevelName;
                    allItem.StLevelId = syslistItem.StLevelId;
                    typeList.Add(allItem);
                }
                return typeList;
            }
            else
            {
                StLevel firstItem = new StLevel();
                firstItem.StLevelName = "全部";
                firstItem.StLevelId = Guid.Empty;
                typeList.Add(firstItem);
            }
            return typeList;
        }

        #region 试题显示方法
        /// <summary>
        /// 系统ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestQuestionId</param>
        /// <returns>返回系统名称</returns>
        public string SystemcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> sysList = null;
            string sysName = "";
            string sqlSelect = string.Format(@"SELECT sysTable.* FROM (SELECT subTable.* FROM StTypes AS subTable
            LEFT JOIN TestQuestions AS questionTable
            ON subTable.StTypeId=questionTable.StTypeId
            WHERE questionTable.TestQuestionId='{0}') AS B
            LEFT JOIN StTypes AS workTable
            ON B.Fid=workTable.StTypeId
            LEFT JOIN StTypes AS sysTable
            ON sysTable.StTypeId=workTable.Fid", gid);
            sysList = dbContext.Database.SqlQuery<StType>(sqlSelect).ToList();
            foreach (var item in sysList)
            {
                sysName = item.Name;
            }
            return sysName;
        }

        /// <summary>
        /// 工种ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestQuestionId</param>
        /// <returns>返回工种名称</returns>
        public string TypeofWorkcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> workList = null;
            string workName = "";
            string sqlSelect = string.Format(@"SELECT workTable.* FROM (SELECT subTable.* FROM StTypes AS subTable
            LEFT JOIN TestQuestions AS questionTable
            ON subTable.StTypeId=questionTable.StTypeId
            WHERE questionTable.TestQuestionId='{0}') AS B
            LEFT JOIN StTypes AS workTable
            ON B.Fid=workTable.StTypeId", gid);
            workList = dbContext.Database.SqlQuery<StType>(sqlSelect).ToList();
            foreach (var item in workList)
            {
                workName = item.Name;
            }
            return workName;
        }

        /// <summary>
        /// 科目ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestQuestionId</param>
        /// <returns>返回科目名称</returns>
        public string SubjectcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> subList = null;
            string subName = "";
            string sqlSelect = string.Format(@"SELECT subTable.* FROM StTypes AS subTable
            LEFT JOIN TestQuestions AS questionTable
            ON subTable.StTypeId=questionTable.StTypeId
            WHERE questionTable.TestQuestionId='{0}'", gid);
            subList = dbContext.Database.SqlQuery<StType>(sqlSelect).ToList();
            foreach (var item in subList)
            {
                subName = item.Name;
            }
            return subName;
        }

        /// <summary>
        /// 类别ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestQuestionId</param>
        /// <returns>返回类别名称</returns>
        public string GenrecmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            string genreName = "";
            var GenreId = dbContext.TestQuestions.Where(c => c.TestQuestionId == gid).FirstOrDefault();
            if (GenreId.StTypeSupplyId != Guid.Empty)
            {
                var GenreName = dbContext.StTypeSupplies.Where(c => c.StTypeSupplyId == GenreId.StTypeSupplyId).FirstOrDefault();
                genreName = GenreName.StTypeName;
                return genreName;
            }
            else
            {
                genreName = "全部";
                return genreName;
            }
        }

        /// <summary>
        /// 等级ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestQuestionId</param>
        /// <returns>返回等级名称</returns>
        public string LevelcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            string LevName = "";
            var LevId = dbContext.TestQuestions.Where(c => c.TestQuestionId == gid).FirstOrDefault();
            if (LevId.StTypeSupplyId != Guid.Empty)
            {
                var LeveName = dbContext.StLevels.Where(c => c.StLevelId == LevId.StLevelId).FirstOrDefault();
                LevName = LeveName.StLevelName;
                return LevName;
            }
            else
            {
                LevName = "全部";
                return LevName;
            }
        }
        #endregion

        #region 试卷显示方法
        /// <summary>
        /// 系统ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestPaperId</param>
        /// <returns>返回系统名称</returns>
        public string pSystemcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> sysList = null;
            string sysName = "";
            string sqlSelect = string.Format(@"SELECT sysTable.* FROM (SELECT subTable.* FROM StTypes AS subTable
            LEFT JOIN TestPapers AS paperTable
            ON subTable.StTypeId=paperTable.StTypeId
            WHERE paperTable.TestPaperId='{0}') AS B
            LEFT JOIN StTypes AS workTable
            ON B.Fid=workTable.StTypeId
            LEFT JOIN StTypes AS sysTable
            ON sysTable.StTypeId=workTable.Fid", gid);
            sysList = dbContext.Database.SqlQuery<StType>(sqlSelect).ToList();
            foreach (var item in sysList)
            {
                sysName = item.Name;
            }
            return sysName;
        }

        /// <summary>
        /// 工种ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestPaperId</param>
        /// <returns>返回工种名称</returns>
        public string pTypeofWorkcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> workList = null;
            string workName = "";
            string sqlSelect = string.Format(@"SELECT workTable.* FROM (SELECT subTable.* FROM StTypes AS subTable
            LEFT JOIN TestPapers AS paperTable
            ON subTable.StTypeId=paperTable.StTypeId
            WHERE paperTable.TestPaperId='{0}') AS B
            LEFT JOIN StTypes AS workTable
            ON B.Fid=workTable.StTypeId", gid);
            workList = dbContext.Database.SqlQuery<StType>(sqlSelect).ToList();
            foreach (var item in workList)
            {
                workName = item.Name;
            }
            return workName;
        }

        /// <summary>
        /// 科目ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestPaperId</param>
        /// <returns>返回科目名称</returns>
        public string pSubjectcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            List<StType> subList = null;
            string subName = "";
            string sqlSelect = string.Format(@"SELECT subTable.* FROM StTypes AS subTable
            LEFT JOIN TestPapers AS paperTable
            ON subTable.StTypeId=paperTable.StTypeId
            WHERE paperTable.TestPaperId='{0}'", gid);
            subList = dbContext.Database.SqlQuery<StType>(sqlSelect).ToList();
            foreach (var item in subList)
            {
                subName = item.Name;
            }
            return subName;
        }

        /// <summary>
        /// 类别ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestPaperId</param>
        /// <returns>返回类别名称</returns>
        public string pGenrecmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            string genreName = "";
            var GenreId = dbContext.TestPapers.Where(c => c.TestPaperId == gid).FirstOrDefault();
            if (GenreId.StTypeSupplyId != Guid.Empty)
            {
                var GenreName = dbContext.StTypeSupplies.Where(c => c.StTypeSupplyId == GenreId.StTypeSupplyId).FirstOrDefault();
                genreName = GenreName.StTypeName;
                return genreName;
            }
            else
            {
                genreName = "全部";
                return genreName;
            }
        }

        /// <summary>
        /// 等级ComboBox显示方法
        /// </summary>
        /// <param name="gid">TestQuestionId</param>
        /// <returns>返回等级名称</returns>
        public string pLevelcmbShow(Guid gid)
        {
            dbContext = new StRttmyContext();
            string LevName = "";
            var LevId = dbContext.TestPapers.Where(c => c.TestPaperId == gid).FirstOrDefault();
            if (LevId.StTypeSupplyId != Guid.Empty)
            {
                var LeveName = dbContext.StLevels.Where(c => c.StLevelId == LevId.StLevelId).FirstOrDefault();
                LevName = LeveName.StLevelName;
                return LevName;
            }
            else
            {
                LevName = "全部";
                return LevName;
            }
        }
        #endregion
    }
}
