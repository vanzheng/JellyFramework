using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGB.Database.Schema
{
    public static class SchemaConstants
    {
        #region Tables

        public const string TABLE_NAME               = "TABLE_NAME";
        public const string TABLE_TYPE               = "TABLE_TYPE";

        #endregion

        #region Columns

        public const string COLUMN_NAME              = "COLUMN_NAME";
        public const string COLUMN_HASDEFAULT        = "COLUMN_HASDEFAULT";
        public const string COLUMN_DEFAULT           = "COLUMN_DEFAULT";

        #endregion

        #region Parameters

        public const string SPECIFIC_NAME            = "SPECIFIC_NAME";
        public const string PARAMETER_MODE           = "PARAMETER_MODE";
        public const string IS_RESULT                = "IS_RESULT";
        public const string PARAMETER_NAME           = "PARAMETER_NAME";

        #endregion

        #region Common

        public const string ORDINAL_POSITION         = "ORDINAL_POSITION";
        public const string IS_NULLABLE              = "IS_NULLABLE";
        public const string DATA_TYPE                = "DATA_TYPE";
        public const string CHARACTER_MAXIMUM_LENGTH = "CHARACTER_MAXIMUM_LENGTH";
        public const string NUMERIC_PRECISION        = "NUMERIC_PRECISION";
        public const string NUMERIC_SCALE            = "NUMERIC_SCALE";
        public const string DESCRIPTION              = "DESCRIPTION";

        #endregion
    }

    public static class CollectionName 
    {
        public const string Tables                   = "Tables";
        public const string Columns                  = "Columns";
        public const string Views                    = "Views";
        public const string Procedures               = "Procedures";
        public const string ProcedureParameters      = "ProcedureParameters";
        public const string Catalog                  = "Catalog";
        public const string Indexes                  = "Indexes";
    }
}
