using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.Models
{
    public class CommentsTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }

        public long ItemID { get; set; }

        public String Comment { get; set; }

        public long UserID { get; set; }

        public CommentsTable()
            : base("")
        {
        }

        public CommentsTable(Object connexionString)
            : base(connexionString)
        {
            SQLTableName = "COMMENTS";
        }
        public override void GetValues()
        {
            ID = long.Parse(FieldsValues[0]);
            ItemID = long.Parse(FieldsValues[1]);
            Comment = FieldsValues[2];
            UserID = long.Parse(FieldsValues[3]);
        }

        public override void Insert()
        {
            InsertRecord(ItemID, Comment, UserID);
        }

        public override void Update()
        {
            UpdateRecord(ID, ItemID, Comment, UserID);
        }
    }
}
