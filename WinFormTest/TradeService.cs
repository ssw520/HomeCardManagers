using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WinFormTest
{
    public class TradeService
    {
        /// <summary>
        /// 存款交易
        /// </summary>
        /// <param name="cardId">卡号</param>
        /// <param name="number">交易金额</param>
        /// <param name="about">交易备注</param>
        public void AddMoney(string cardId, double number, string about)
        {

            string date = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
            ///SQL注入式攻击
            //string sql = string.Format("insert into trade(cardId,tradeTime,tradeType,number,about) " +
            //    " values('{0}','{1}','{2}',{3},'{4}')", cardId, date, "存钱", number, about);

            string sql = "insert into trade(cardId,tradeTime,tradeType,number,about) values(@cardId," +
                "@tradeTime,@tradeType,@number,@about)";
            SqlParameter[] values = new SqlParameter[]{
                new SqlParameter("@cardId",cardId),
                new SqlParameter("@tradeTime",date),
                new SqlParameter("@tradeType","存钱"),
                new SqlParameter("@number",number),
                new SqlParameter("@about",about)
            };

            int ok = DBHelper.executeCommand(sql,values);
            if (ok > 0)
            {
                Console.WriteLine("执行成功");
            }
            else
            {
                Console.WriteLine("执行失败");
            }
        }
        /// <summary>
        /// 取款交易
        /// </summary>
        /// <param name="cardId">卡号</param>
        /// <param name="number">交易金额</param>
        /// <param name="about">交易备注</param>
        public void Trade(string cardId, double number, string about)
        {

            string date = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
            string sql = string.Format("insert into trade(cardId,tradeTime,tradeType,number,about) " +
                " values('{0}','{1}','{2}',{3},'{4}')", cardId, date, "取钱", number, about);

            int ok = DBHelper.executeCommand(sql);
            if (ok > 0)
            {
                Console.WriteLine("执行成功");
            }
            else
            {
                Console.WriteLine("执行失败");
            }

        }
        /// <summary>
        /// 查看交易明细
        /// </summary>
        /// <param name="cardId">卡号</param>
        public void LookTradeInfo(string cardId)
        {
            try
            {
                string date = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
               // string sql = string.Format("select * from trade where cardId='{0}'", cardId);
                string sql = "select * from trade where cardId=@cardId";
                SqlParameter[] values = new SqlParameter[]{
                    new SqlParameter("@cardId",cardId)
                };

                DataTable dt = DBHelper.GetDataTable(sql,values);

                Console.WriteLine("卡号 \t 交易时间 \t 交易类型 \t 交易金额");

                foreach(DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["cardId"] + " \t " + dr["tradeTime"] + " \t " +
                        dr["tradeType"] + " \t " + dr["number"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void LookBalance(string cardId)
        {

        }
    }
}
