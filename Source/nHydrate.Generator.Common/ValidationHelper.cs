#region Copyright (c) 2006-2019 nHydrate.org, All Rights Reserved
// -------------------------------------------------------------------------- *
//                           NHYDRATE.ORG                                     *
//              Copyright (c) 2006-2019 All Rights reserved                   *
//                                                                            *
//                                                                            *
// Permission is hereby granted, free of charge, to any person obtaining a    *
// copy of this software and associated documentation files (the "Software"), *
// to deal in the Software without restriction, including without limitation  *
// the rights to use, copy, modify, merge, publish, distribute, sublicense,   *
// and/or sell copies of the Software, and to permit persons to whom the      *
// Software is furnished to do so, subject to the following conditions:       *
//                                                                            *
// The above copyright notice and this permission notice shall be included    *
// in all copies or substantial portions of the Software.                     *
//                                                                            *
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,            *
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES            *
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  *
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY       *
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,       *
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE          *
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                     *
// -------------------------------------------------------------------------- *
#endregion
using System.Linq;

namespace nHydrate.Generator.Common
{
    public static class ValidationHelper
    {
        public static readonly string ErrorTextInvalidIdentifier = "This identifier '{0}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidCompany = "The company name is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidCompanyAbbreviation = "This company abbreviation is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidProject = "The project name is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidDatabase = "This database name is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidCodeFacade = "The code facade is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextSQLRequired = "The SQL text is required.";
        public static readonly string ErrorTextPackageRequiresDisplayName = "Package objects must have a display name!";
        public static readonly string ErrorTextDuplicateName = "An object named '{0}' is already defined in this scope.";
        public static readonly string ErrorTextComponentTableDuplicateName = "The component named '{0}' has a duplicate name with a table.";
        public static readonly string ErrorTextDuplicateCodeFacade = "The object named '{0}' has a conflicting name or codefacade in this scope.";
        public static readonly string ErrorTextColumnsRequired = "The object must have at least one generated column.";
        public static readonly string ErrorTextNoPrimaryKey = "One or more columns must be marked as a primary key and be generated.";
        public static readonly string ErrorTextPrimaryKeyNull = "A primary key cannot allow null values. AllowNull must be false.";
        public static readonly string ErrorTextInvalidIdentityColumn = "Columns marked as identity must be of type Int, BigInt, SmallInt, or UniqueIdentifier";
        public static readonly string ErrorTextTypeTableNoData = "Type tables must have static data defined.";
        public static readonly string ErrorTextRelationshipTypeMismatch = "Columns that make-up relationships must be of the same type.";
        public static readonly string ErrorTextDuplicateRoleName = "All relations must have a unique role name for table {0} (and its base hierarchy) and {1}.";
        public static readonly string ErrorTextRelationMustHaveParentChild = "All relations must have a parent and child table defined.";
        public static readonly string ErrorTextTableColumnNameMatch = "A column cannot have the same name as its parent table.";
        public static readonly string ErrorTextColumnDuplicateNameMatch = "Duplicate column name.";
        public static readonly string ErrorTextPreDefinedNameField = "The column '{0}' cannot use the predefined name.";
        public static readonly string ErrorTextInvalidInheritance = "The inheritance chain is not valid. All tables must have the same primary keys in type and name and other fields must have unique names across the inheritance chain.";
        public static readonly string ErrorTextNoTables = "There must be one or more tables defined.";
        public static readonly string ErrorTextNoColumns = "All tables must one or more columns defined.";
        public static readonly string ErrorTextMinMaxValueMismatch = "The minimum value cannot be greater than the maximum value.";
        public static readonly string ErrorTextMinMaxValueInvalidType = "The minimum and maximum values can only be set for numeric fields.";
        public static readonly string ErrorTextTableDoesNotAllowModification = "The table '{0}' does not allow modification so cannot be unit tested.";
        public static readonly string ErrorTextTableColumnNonPrimaryRelationNotUnique = "The column {0} in table '{1}' must be marked unique because there is a non-primary key relationship that depends on it.";
        public static readonly string ErrorTextTableProjectSameName = "The table '{0}' cannot have the same name as the project.";
        public static readonly string ErrorTextComponentProjectSameName = "The component '{0}' cannot have the same name as the project.";
        public static readonly string ErrorTextTableCompositeNoColumns = "The table composite '{0}' must have at least one column.";
        public static readonly string ErrorTextTableComponentNoColumns = "The table component '{0}' must have at least one column.";
        public static readonly string ErrorTextMutableInherit = "The table '{0}' is mutable and it inherited from the immutable table '{1}'. This must be changed.";
        public static readonly string ErrorTextTypeTableIsMutable = "The type table '{0}' must be marked as immutable.";
        public static readonly string ErrorTextBaseTableNonGenerated = "The base table '{0}' of table '{1}' is not marked for generation.";
        public static readonly string ErrorTextConflictingRelationships = "The following tables have conflicting relationships: {0}. Delete one or more relationships or assign role names to them.";
        public static readonly string ErrorTextComponentMustHaveTablePK = "The table component {0} must include the primary keys for from the parent table.";
        public static readonly string ErrorTextChildTableRelationIdentity = "The relationship is based on an identity column in the table '{0}' mapping to another identity column in table '{1}'. This is not valid.";
        public static readonly string ErrorTextNameConfictsWithGeneratedCode = "The object '{0}' will conflict with generated code. Please change the name.";
        public static readonly string ErrorTextParentTableNoRelation = "The parent table '{0}' of '{1}' must have a relation to its child.";
        public static readonly string ErrorTextTypeTablePrimaryKey = "The type table '{0}' must have a single primary key of an integer type.";
        public static readonly string ErrorTextTypeTableTextField = "The type table '{0}' must have a 'Name' or 'Description' field to build the enumeration type.";
        public static readonly string ErrorTextTypeTableStaticDataEmpty = "The type table '{0}' must have static data set for the primary key and the Name/Description field.";
        public static readonly string ErrorTextAuditFieldMatchBase = "The audit settings for table '{0}' must match those of all base tables.";
        public static readonly string ErrorTextSelfRefChildColumnPK = "The self-referential relationship cannot map to the table primary key.";
        public static readonly string ErrorTextSelfRefMustHaveRole = "The self-referential relationship must have a role name.";
        public static readonly string ErrorTextAssociativeTableMustHave2Relations = "The associative table '{0}' must be the child of exactly two relations. It currently has {1}.";
        public static readonly string ErrorTextDataTypeNotSupported = "The column '{0}' has an unsupported datatype for the target version of SQL Server.";
        public static readonly string ErrorTextColumnDefault = "The default for '{0}' column is invalid based on its datatype.";
        public static readonly string ErrorTextColumnMaxNotSupported = "The length (max) setting is not supported in the target version of SQL Server.";
        public static readonly string ErrorTextColumnLengthNotZero = "The length of a column cannot be zero.";
        public static readonly string ErrorTextIdentityPKNotOnlyKey = "The table '{0}' has a database identity primary key and a composite primary key.";
        public static readonly string ErrorTextDuplicateStaticData = "The static data value '{0}' is duplicated for table {1}.";
        public static readonly string ErrorTextIdentityOnlyOnePerTable = "There can be only one database identity column per table.";
        public static readonly string ErrorTextColumnDecimalPrecision = "The decimal column '{0}' must have length from 1-38.";
        public static readonly string ErrorTextColumnDecimalScale = "The decimal column '{0}' must have a scale from 0 to the length value.";
        public static readonly string ErrorTextDuplicateRelation = "The relation '{0}' is duplicated. A relation can only exist once in a model.";
        public static readonly string ErrorTextDuplicateRelationFullHierarchy = "A relation is duplicated between the table {0} or one of its bases and the table {1}.";
        public static readonly string ErrorTextSelfRefOnlyOne = "The table {0} has more than one self-referencing relationship.";
        public static readonly string ErrorTextMultiFieldRelationsMapDifferentFields = "Multi-field relationships must map to unique fields.";
        public static readonly string ErrorTextComputeColumnNoFormula = "The computed column '{0}' must have a formula.";
        public static readonly string ErrorTextComputeNonColumnHaveFormula = "The column '{0}' cannot have a formula set because it is not a computed column.";
        public static readonly string ErrorTextComputeColumnNoPK = "The column '{0}' is computed and cannot by a primary key.";
        public static readonly string ErrorTextAssociativeTableNotImmutable = "An associative table cannot be immutable.";
        public static readonly string ErrorTextAssociativeTableNotInherited = "The associative table '{0}', cannot be inherited from another table.";
        public static readonly string ErrorTextRelationFieldNotMatchAssociatedTable = "The relation {0}->{1} cannot have the foreign key field name the same as the parent table.";
        public static readonly string ErrorTextTableNotHave1IdentityOnly = "The table '{0}' must have at least one non-identity column or be marked immutable.";
        public static readonly string ErrorTextAuditFieldsNotUnique = "The audit field names must be unique for created, modified, and timestamps.";
        public static readonly string ErrorTextRoleNoStartNumber = "Role names cannot start with a number.";
        public static readonly string ErrorTextInvalidNamespace = "The defined namespace is not valid. It must be in the format A.B.*.";
        public static readonly string ErrorTextColumnCannotHaveDefault = "The column '{0}' cannot have a default value";
        public static readonly string ErrorTextColumnInvalidDefault = "The column '{0}' possibly has an invalid default value";
        public static readonly string ErrorTextExistingSPNeedsDBName = "The stored procedure '{0}' is marked as existing and must have a defined DatabaseObjectName";
        public static readonly string ErrorTextRelationM_NRoleMismatch = "The role names for the two incomming relations for the associative table '{0}' must match";
        public static readonly string ErrorTextRelationM_NNameDuplication = "Two associative tables are defined between the same outer tables '{0}' and '{1}' with the same role name. You must use a unique role name for the relations.";
        public static readonly string ErrorTextTableAssociativeNoAuditTracking = "The associative table {0} cannot have audit traking enabled.";
        public static readonly string ErrorTextTableBadStaticData = "One or more static data entries cannot be converted to specified data type.";
        public static readonly string ErrorTextDecimalColumnTooSmall = "The decimal column {0} has a very small length of {1}. This may be a mistake.";
        public static readonly string ErrorTextRelationCausesNameConflict = "A M:N relation for entity '{0}' causes a naming conflict in another relation with entity '{1}'";
        public static readonly string ErrorTextRelationDuplicate = "There are 2 or more relations on the entity '{0}' to the same target table on the same fields.";
        public static readonly string ErrorTextTableAssociativeNeedsNonOverlappingColumns = "The associative table {0} must contain the primary keys of the parent tables with no overlapping columns.";

        public const string ValidCodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        //private const string reservedSQLWords = "ENCRYPTION,ORDER,ADD,END,OUTER,ALL,ERRLVL,OVER,ALTER,ESCAPE,PERCENT,AND,EXCEPT,PLAN,ANY,EXEC,PRECISION,AS,EXECUTE,PRIMARY,ASC,EXISTS,PRINT,AUTHORIZATION,EXIT,PROC,AVG,EXPRESSION,PROCEDURE,BACKUP,FETCH,PUBLIC,BEGIN,FILE,RAISERROR,BETWEEN,FILLFACTOR,READ,BREAK,FOR,READTEXT,BROWSE,FOREIGN,RECONFIGURE,BULK,FREETEXT,REFERENCES,BY,FREETEXTTABLE,REPLICATION,CASCADE,FROM,RESTORE,CASE,FULL,RESTRICT,CHECK,RETURN,CHECKPOINT,GOTO,REVOKE,CLOSE,GRANT,RIGHT,CLUSTERED,GROUP,ROLLBACK,COALESCE,HAVING,ROWCOUNT,COLLATE,HOLDLOCK,ROWGUIDCOL,COLUMN,IDENTITY,RULE,COMMIT,IDENTITY_INSERT,SAVE,COMPUTE,IDENTITYCOL,SCHEMA,CONSTRAINT,IF,SELECT,CONTAINS,IN,SESSION_USER,CONTAINSTABLE,INDEX,SET,CONTINUE,INNER,SETUSER,CONVERT,INSERT,SHUTDOWN,COUNT,INTERSECT,SOME,CREATE,INTO,STATISTICS,CROSS,IS,SUM,CURRENT,JOIN,SYSTEM_USER,CURRENT_DATE,KEY,TABLE,CURRENT_TIME,KILL,TEXTSIZE,CURRENT_TIMESTAMP,LEFT,THEN,CURRENT_USER,LIKE,TO,CURSOR,LINENO,TOP,DATABASE,LOAD,TRAN,DATABASEPASSWORD,MAX,TRANSACTION,DATEADD,MIN,TRIGGER,DATEDIFF,NATIONAL,TRUNCATE,DATENAME,NOCHECK,TSEQUAL,DATEPART,NONCLUSTERED,UNION,DBCC,NOT,UNIQUE,DEALLOCATE,NULL,UPDATE,DECLARE,NULLIF,UPDATETEXT,DEFAULT,OF,USE,DELETE,OFF,USER,DENY,OFFSETS,VALUES,DESC,ON,VARYING,DISK,OPEN,VIEW,DISTINCT,OPENDATASOURCE,WAITFOR,DISTRIBUTED,OPENQUERY,WHEN,DOUBLE,OPENROWSET,WHERE,DROP,OPENXML,WHILE,DUMP,OPTION,WITH,ELSE,OR,WRITETEXT";
        private const string reservedSQLWords = "";
        //REMOVED: region
        //private const string reservedCSharpWords = "_Bool,_Complex,_Imaginary,_Packed,abstract,as,auto,base,bool,break,byte,case,catch,char,checked,class,const,continue,decimal,default,delegate,do,double,else,enum,event,exception,explicit,extern,false,finally,fixed,float,for,foreach,goto,if,implicit,in,inline,int,interface,internal,is,lock,long,namespace,new,null,object,operator,out,override,params,private,protected,public,readonly,ref,register,restrict,return,sbyte,sealed,short,signed,sizeof,stackalloc,static,string,struct,switch,this,throw,true,try,typedef,typeof,uint,ulong,unchecked,union,unsafe,unsigned,ushort,using,virtual,void,volatile,while,PrimaryKey,attribute,businessobject,attribute,system,BusinessCollectionBase,BusinessCollectionPersistableBase,BusinessObjectBase,BusinessObjectList,BusinessObjectPersistableBase,IVisitee,IVisitor,IAuditable,IPagingObject,IPagingFieldItem,DomainCollectionBase,Enumerations,IBusinessCollection,IBusinessObject,IBusinessObjectSearch,IDomainCollection,IDomainObject,IErrorRow,IPersistableBusinessCollection,IPersistableBusinessObject,IPersistableDomainObject,IPrimaryKey,IPropertyBag,IPropertyDefine,IReadOnlyBusinessCollection,IReadOnlyBusinessObject,ITyped,IWrappingClass,PersistableDomainCollectionBase,PersistableDomainObjectBase,PrimaryKey,ReadOnlyDomainCollection,ReadOnlyDomainObjectBase,SelectCommand,StoredProcedure,StoredProcedureFactory,SubDomainBase,SubDomainWrapper,BusinessObjectCancelEventArgs,BusinessObjectEventArgs,ConcurrencyException,DefaultLogConfiguration,ILogClass,ILogConfiguration,MultiProcessTraceListener,TraceSwitchAttribute,IXmlable,IBoolFieldDescriptor,IDateFieldDescriptor,IFieldDescriptor,IFloatFieldDescriptor,IIntegerFieldDescriptor,IStringFieldDescriptor,As,Assembly,Auto,Base,Boolean,ByRef,Byte,ByVal,Call,Case,Catch,CBool,CByte,CChar,CDate,CDec,CDbl,Char,CInt,Class,CLng,CObj,Const,CShort,CSng,CStr,CType,Date,Decimal,Declare,Default,Delegate,Dim,Do,Double,Each,Else,ElseIf,End,Enum,Erase,Error,Event,Exit,ExternalSource,False,Finalize,Finally,Float,For,Friend,Get,GetType,Goto,Handles,If,Implements,Imports,In,Inherits,Integer,Interface,Is,Let,Lib,Like,Long,Loop,Me,Mod,MustInherit,MustOverride,MyBase,MyClass,Namespace,New,Next,Not,Nothing,NotInheritable,NotOverridable,Object,On,Option,Optional,Or,Overloads,Overridable,Overrides,ParamArray,Preserve,Private,Property,Protected,Public,RaiseEvent,ReadOnly,ReDim,REM,RemoveHandler,Resume,Return,Select,Set,Shadows,Shared,Short,Single,Static,Step,Stop,String,Structure,Sub,SyncLock,Then,Throw,To,True,Try,TypeOf,Unicode,Until,volatile,When,While,With,WithEvents,WriteOnly,Xor,eval,extends,instanceof,package,var";
        private const string reservedCSharpWords = "_Bool,_Complex,_Imaginary,_Packed,abstract,as,auto,base,bool,break,byte,case,catch,char,checked,class,const,continue,decimal,default,delegate,do,double,else,enum,event,exception,explicit,extern,false,finally,fixed,float,for,foreach,goto,if,implicit,in,inline,int,interface,internal,is,lock,long,namespace,new,null,object,operator,out,override,params,private,protected,public,readonly,ref,register,restrict,return,sbyte,sealed,short,signed,sizeof,stackalloc,static,string,struct,switch,this,throw,true,try,typedef,typeof,uint,ulong,unchecked,union,unsafe,unsigned,ushort,using,virtual,void,volatile,while,PrimaryKey,attribute,businessobject,attribute,system,BusinessCollectionBase,BusinessCollectionPersistableBase,BusinessObjectBase,BusinessObjectList,BusinessObjectPersistableBase,IVisitee,IVisitor,IAuditable,IPagingObject,IPagingFieldItem,DomainCollectionBase,Enumerations,IBusinessCollection,IBusinessObject,IBusinessObjectSearch,IDomainCollection,IDomainObject,IErrorRow,IPersistableBusinessCollection,IPersistableBusinessObject,IPersistableDomainObject,IPrimaryKey,IPropertyBag,IPropertyDefine,IReadOnlyBusinessCollection,IReadOnlyBusinessObject,ITyped,IWrappingClass,PersistableDomainCollectionBase,PersistableDomainObjectBase,PrimaryKey,ReadOnlyDomainCollection,ReadOnlyDomainObjectBase,SelectCommand,StoredProcedure,StoredProcedureFactory,SubDomainBase,SubDomainWrapper,BusinessObjectCancelEventArgs,BusinessObjectEventArgs,ConcurrencyException,DefaultLogConfiguration,ILogClass,ILogConfiguration,MultiProcessTraceListener,TraceSwitchAttribute,IXmlable,IBoolFieldDescriptor,IDateFieldDescriptor,IFieldDescriptor,IFloatFieldDescriptor,IIntegerFieldDescriptor,IStringFieldDescriptor,As,Assembly,Auto,Base,Boolean,ByRef,Byte,ByVal,Call,Case,Catch,CBool,CByte,CChar,CDate,CDec,CDbl,Char,CInt,Class,CLng,CObj,Const,CShort,CSng,CStr,CType,Date,Decimal,Declare,Default,Delegate,Dim,Do,Double,Enum,Erase,Error,Event,Exit,ExternalSource,False,Finalize,Finally,Float,For,Friend,Get,GetType,Handles,If,Implements,Imports,In,Inherits,Integer,Interface,Is,Let,Lib,Like,Long,Loop,Me,Mod,Namespace,New,Next,Not,Nothing,Object,On,Option,Optional,Or,Preserve,Private,Property,Protected,Public,RaiseEvent,ReadOnly,ReDim,REM,RemoveHandler,Return,Select,Set,Shadows,Shared,Short,Single,Static,Step,Stop,String,Structure,Sub,SyncLock,Then,Throw,To,True,Try,TypeOf,Unicode,Until,volatile,When,While,With,WithEvents,WriteOnly,Xor,eval,extends,instanceof,package,var";
        private const string reservedFields = "clone,isparented,container,delete,equals,getdatetime,getdefault,getdouble,getfieldlength,GetFriendlyName,GetHashCode,GetInteger,GetString,GetValue,IsEquivalent,ItemState,OnValidate,ParentCollection,Persist,RejectChanges,PrimaryKey,ReleaseNonIdentifyingRelationships,Remove,SelectUsingPK,SetCreatedDate,SetModifiedDate,SetValue,Validate,wrappedClass,DeleteData,GetDatabaseFieldName,GetFieldAliasFromFieldNameSqlMapping,GetMaxLength,GetPagedSQL,GetRemappedLinqSql,GetTableFromFieldAliasSqlMapping,GetTableFromFieldNameSqlMapping,UpdateData";

        public static bool ValidDatabaseIdenitifer(string name)
        {
            if (name.Length == 0)
                return false;

            var words = reservedSQLWords.ToLower().Split(',');

            var q = (from x in words
                     where name.ToLower() == x
                     select x).FirstOrDefault();

            if (q != null)
                return false;

            //The database does actualy allow spaces
            var validchars2 = ValidCodeChars + " /#";

            foreach (var c in name)
            {
                if (validchars2.IndexOf(c) == -1)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines if the specified value is a valid C# token
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ValidCodeIdentifier(string name)
        {
            if (name.Length == 0)
                return false;

            var words = reservedCSharpWords.ToLower().Split(',');

            var q = (from x in words
                     where name.ToLower() == x
                     select x).FirstOrDefault();

            if (q != null)
                return false;

            foreach (var c in name)
            {
                if (ValidCodeChars.IndexOf(c) == -1)
                    return false;
            }

            //First 
            var firstChar = name.First();
            int n;
            if (int.TryParse(firstChar.ToString(), out n))
                return false;

            return true;
        }

        /// <summary>
        /// Determines if the specified value matches any reserved words for objects
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ValidFieldIdentifier(string name)
        {
            if (name.Length == 0)
                return false;

            var words = reservedFields.ToLower().Split(',');

            var q = (from x in words
                     where name.ToLower() == x
                     select x).FirstOrDefault();

            if (q != null)
                return false;

            foreach (var c in name)
            {
                if (ValidCodeChars.IndexOf(c) == -1)
                    return false;
            }

            //First 
            var firstChar = name.First();
            int n;
            if (int.TryParse(firstChar.ToString(), out n))
                return false;

            return true;
        }

        public static string MakeCodeIdentifer(string name)
        {
            if (name.Length == 0)
                return "";

            var retval = string.Empty;
            var validchars2 = ValidCodeChars;

            foreach (var c in name)
            {
                if (validchars2.IndexOf(c) == -1)
                    retval += "_";
                else
                    retval += c;
            }
            return retval;
        }

        public static string MakeDatabaseIdentifier(string name)
        {
            if (name.Length == 0)
                return "";

            var retval = string.Empty;
            var validchars2 = ValidCodeChars + " /#";

            foreach (var c in name)
            {
                if (validchars2.IndexOf(c) == -1)
                    retval += "_";
                else
                    retval += c;
            }
            return retval;
        }

        public static string MakeDatabaseScriptIdentifier(string name)
        {
            if (name.Length == 0)
                return "";

            var retval = string.Empty;
            foreach (var c in name)
            {
                if (ValidCodeChars.IndexOf(c) == -1)
                    retval += "_";
                else
                    retval += c;
            }
            return retval;
        }

        public static bool IsValidNamespace(string namespaceValue)
        {
            if (namespaceValue != namespaceValue.Trim()) return false;
            var arr = namespaceValue.Split('.');
            if (arr.Length == 0) return false;

            foreach (var s in arr)
            {
                if (!ValidCodeIdentifier(s)) return false;
            }

            return true;
        }

    }
}