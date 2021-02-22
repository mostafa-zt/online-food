using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFood.Common.Model
{
    /// <summary>
    /// this class provides a model to use with JQuery DataTables plugin
    /// </summary>
    public class jQueryDataTableParamModel
    {
        #region DataTable specific properties
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string draw { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int? length { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int? start { get; set; }
        #endregion

        #region Custom properties
        /// <summary>
        ///  فیلد های اضافی که به صورت پارامتر به اکشن فرستاده میشود در اینجا نوشته میشود
        /// </summary>
        //public int MyProperty { get; set; }
        #endregion
    }
}