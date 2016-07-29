using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TODOTask.Manager.Utilities
{
    public enum LableItem
    {
        Page_Index,
        Page_List,
        Page_Detail,
        Page_Create,
        Page_Edit,
        Page_Delete,
        Task_SettleEvent,
        Task_TODOList,
    }
    public enum Culture
    {
        CN,
        EN,
    }
    public static class Constants
    {
        /// <summary>
        /// 通用的系统文化类型,可以为个人定制
        /// </summary>
        public static Culture SysCulture { set; get; }
        public static string GetText(LableItem lableItem)
        {
            string value = "";
            switch (SysCulture)
            {
                case Culture.CN:
                    value = lableItem.GetCN();
                    break;
                case Culture.EN:
                default:
                    throw new NotImplementedException("暂未实现功能");
            }
            return value;
        }
    }
    public static class CNAppender
    {
        public static string GetCN(this LableItem lableItem)
        {
            switch (lableItem)
            {
                case LableItem.Page_Index:
                    return "索引页";
                case LableItem.Page_List:
                    return "列表页";
                case LableItem.Page_Detail:
                    return "详情页";
                case LableItem.Page_Create:
                    return "创建页";
                case LableItem.Page_Edit:
                    return "编辑页";
                case LableItem.Page_Delete:
                    return "删除页";
                case LableItem.Task_SettleEvent:
                    return "处理事件";
                case LableItem.Task_TODOList:
                    return "代办任务列表";
                default:
                    return "";
            }
        }
    }
}