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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nHydrate.Dsl
{
    public static class ValidationHelper
    {
        public static readonly string ErrorTextInvalidIdentifier = "The identifier '{0}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierComposite = "The composite identifier '{0}' for entity '{1}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierSelectCommand = "The select command identifier '{0}' for entity '{1}' is the wrong format, contains invalid characters, or is a reserved word.";

        public static readonly string ErrorTextInvalidIdentifierSPParam = "The parameter identifier '{0}' for stored procedure '{1}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierSPField = "The field identifier '{0}' for stored procedure '{1}' is the wrong format, contains invalid characters, or is a reserved word.";

        public static readonly string ErrorTextInvalidIdentifierViewField = "The field identifier '{0}' for view '{1}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierFuncParam = "The parameter identifier '{0}' for function '{1}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierFuncField = "The field identifier '{0}' for function '{1}' is the wrong format, contains invalid characters, or is a reserved word.";

        public static readonly string ErrorTextInvalidIdentifierModule = "The module identifier '{0}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierView = "The view identifier '{0}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidFieldIdentifierView = "The view {0} has an invalid field identifier '{1}' that is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidIdentifierSP = "The stored procedure identifier '{0}' is the wrong format, contains invalid characters, or is a reserved word.";
        public static readonly string ErrorTextInvalidCompany = "The company name is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidCompanyAbbreviation = "This company abbreviation is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidProject = "The project name is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidDatabase = "This database name is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextInvalidCodeFacade = "The code facade is the wrong format or contains invalid characters.";
        public static readonly string ErrorTextSQLRequiredSelectCommand = "The SQL text is required for the select command '{0}'.";
        public static readonly string ErrorTextSQLRequiredFunction = "The SQL text is required for the function '{0}'.";
        public static readonly string ErrorTextSQLRequiredView = "The SQL text is required for the view '{0}'.";
        public static readonly string ErrorTextSQLRequiredStoredProcedure = "The SQL text is required for the stored procedure '{0}'.";
        public static readonly string ErrorTextPackageRequiresDisplayName = "Package objects must have a display name!";
        public static readonly string ErrorTextDuplicateName = "An object named '{0}' is already defined in this scope.";
        public static readonly string ErrorTextComponentTableDuplicateName = "The component named '{0}' has a duplicate name with a entity.";
        public static readonly string ErrorTextDuplicateCodeFacade = "The object named '{0}' has a conflicting name or codefacade in this scope.";
        public static readonly string ErrorTextColumnsRequired = "The object must have at least one generated field.";
        public static readonly string ErrorTextNoPrimaryKey = "The entity '{0}' must have one or more columns marked as a primary key.";
        public static readonly string ErrorTextPrimaryKeyNull = "A primary key cannot allow null values. AllowNull must be false.";
        public static readonly string ErrorTextInvalidIdentityColumn = "Columns marked as identity must be of type Int, BigInt, SmallInt, or UniqueIdentifier";
        public static readonly string ErrorTextTypeTableNoData = "Type tables must have static data defined.";
        public static readonly string ErrorTextRelationshipTypeMismatch = "Columns that make-up relationships must be of the same type.";
        public static readonly string ErrorTextDuplicateRoleName = "All relations must have a unique role name for entity '{0}' (and its base hierarchy) and {1}.";
        public static readonly string ErrorTextRelationMustHaveParentChild = "All relations must have a parent and child entity defined.";
        public static readonly string ErrorTextTableColumnNameMatch = "The field '{0}' cannot have the same name as its parent object {1}.";
        public static readonly string ErrorTextColumnDuplicateNameMatch = "Duplicate parameter name.";
        public static readonly string ErrorTextPreDefinedNameField = "The parameter '{0}' cannot use the predefined name.";
        public static readonly string ErrorTextInvalidInheritance = "The inheritance chain is not valid. All entities must have the same primary keys in type and name and other fields must have unique names across the inheritance chain.";
        public static readonly string ErrorTextNoTables = "There must be one or more entities defined.";
        public static readonly string ErrorTextNoColumns = "All entities must one or more columns defined.";
        public static readonly string ErrorTextMinMaxValueMismatch = "The minimum value cannot be greater than the maximum value.";
        public static readonly string ErrorTextMinMaxValueInvalidType = "The minimum and maximum values can only be set for numeric fields.";
        public static readonly string ErrorTextTableDoesNotAllowModification = "The entity '{0}' does not allow modification so cannot be unit tested.";
        public static readonly string ErrorTextTableColumnNonPrimaryRelationNotUnique = "The field '{0}' in entity '{1}' must be marked unique because there is a non-primary key relationship that depends on it.";
        public static readonly string WarningTextTableColumnNonPrimaryRelationNotUnique = "The field '{0}' in entity '{1}' is marked unique but is not a primary key in a relationship. There will be no Entity Framework navigation property for this relationship.";
        public static readonly string ErrorTextTableProjectSameName = "The entity '{0}' cannot have the same name as the project.";
        public static readonly string ErrorTextComponentProjectSameName = "The component '{0}' cannot have the same name as the project.";
        public static readonly string ErrorTextTableCompositeNoColumns = "The entity composite '{0}' must have at least one parameter.";
        public static readonly string ErrorTextTableComponentNoColumns = "The entity component '{0}' must have at least one parameter.";
        public static readonly string ErrorTextMutableInherit = "The entity '{0}' is mutable and it inherited from the immutable entity '{1}'. This must be changed.";
        public static readonly string ErrorTextTypeTableIsMutable = "The type entity '{0}' must be marked as immutable.";
        public static readonly string ErrorTextBaseTableNonGenerated = "The base entity '{0}' of entity '{1}' is not marked for generation.";
        public static readonly string ErrorTextConflictingRelationships = "The following entities have conflicting relationships: {0}. Delete one or more relationships or assign role names to them.";
        public static readonly string ErrorTextComponentMustHaveTablePK = "The entity component '{0}' must include the primary keys for from the parent entity.";
        public static readonly string ErrorTextChildTableRelationIdentity = "The relationship is based on an identity parameter in the entity '{0}' mapping to another identity parameter in entity '{1}'. This is not valid.";
        public static readonly string ErrorTextNameConfictsWithGeneratedCode = "The object '{0}' will conflict with generated code. Please change the name.";
        public static readonly string ErrorTextParentTableNoRelation = "The parent entity '{0}' of '{1}' must have a relation to its child.";
        public static readonly string ErrorTextTypeTablePrimaryKey = "The type entity '{0}' must have a single primary key of an integer type.";
        public static readonly string ErrorTextTypeTableTextField = "The type entity '{0}' must have a 'Name' or 'Description' parameter to build the enumeration type.";
        public static readonly string ErrorTextTypeTableStaticDataEmpty = "The type entity '{0}' must have static data set for the primary key and the Name/Description parameter.";
        public static readonly string ErrorTextAuditFieldMatchBase = "The audit settings for entity '{0}' must match those of all base entities.";
        public static readonly string ErrorTextSelfRefChildColumnPK = "The self-referential relationship cannot map to the entity primary key.";
        public static readonly string ErrorTextSelfRefMustHaveRole = "The self-referential relationship must have a role name.";
        public static readonly string ErrorTextAssociativeTableMustHave2Relations = "The associative entity '{0}' must be the child of exactly two relations. It currently has {1}.";
        public static readonly string ErrorTextDataTypeNotSupported = "The parameter '{0}' has an unsupported datatype for the target version of SQL Server.";
        public static readonly string ErrorTextColumnDefault = "The default for '{0}' parameter is invalid based on its datatype.";
        public static readonly string ErrorTextColumnMaxNotSupported = "The length (max) setting is not supported in the target version of SQL Server.";
        public static readonly string ErrorTextColumnLengthNotZero = "The length of a parameter cannot be zero.";
        public static readonly string ErrorTextIdentityPKNotOnlyKey = "The entity '{0}' has a database identity primary key and a composite primary key.";
        public static readonly string ErrorTextDuplicateStaticData = "There are duplicate static data values (key or value) for entity {0}.";
        public static readonly string ErrorTextIdentityOnlyOnePerTable = "The entity '{0}' can have only one database identity parameter.";
        public static readonly string ErrorTextColumnDecimalPrecision = "The decimal parameter '{0}' must have length from 1-38.";
        public static readonly string ErrorTextColumnDecimalScale = "The decimal parameter '{0}' must have a scale from 0 to the length value.";
        public static readonly string ErrorTextDuplicateRelation = "The relation '{0}' between entity {1} and {2} is duplicated. Multiple relations between two entities must have unique role names.";
        public static readonly string ErrorTextDuplicateRelationFullHierarchy = "A relation is duplicated between the entity '{0}' or one of its bases and the entity {1}.";
        public static readonly string ErrorTextSelfRefOnlyOne = "The entity '{0}' has more than one self-referencing relationship.";
        public static readonly string ErrorTextMultiFieldRelationsMapDifferentFields = "A multi-parameter relationship from {0}->{1} is duplicated.";
        public static readonly string ErrorTextComputeColumnNoFormula = "The computed parameter '{0}' must have a formula.";
        public static readonly string ErrorTextComputeNonColumnHaveFormula = "The parameter '{0}' cannot have a formula set because it is not a computed parameter.";
        public static readonly string ErrorTextComputeColumnNoPK = "The parameter '{0}' is computed and cannot by a primary key.";
        public static readonly string ErrorTextAssociativeTableNotImmutable = "An associative entity cannot be immutable.";
        public static readonly string ErrorTextAssociativeTableNotInherited = "The associative entity '{0}', cannot be inherited from another entity.";
        public static readonly string ErrorTextRelationFieldNotMatchAssociatedTable = "The relation {0}->{1} cannot have the foreign key parameter name the same as the parent entity.";
        public static readonly string ErrorTextTableNotHave1IdentityOnly = "The entity '{0}' must have at least one non-identity parameter or be marked immutable.";
        public static readonly string ErrorTextAuditFieldsNotUnique = "The audit parameter names must be unique for created, modified, and timestamps.";
        public static readonly string ErrorTextRoleNoStartNumber = "Role names cannot start with a number.";
        public static readonly string ErrorTextInvalidNamespace = "The defined namespace is not valid. It must be in the format A.B.*.";
        public static readonly string ErrorTextColumnCannotHaveDefault = "The parameter '{0}' cannot have a default value";
        public static readonly string ErrorTextColumnInvalidDefault = "The parameter '{0}' possibly has an invalid default value";
        public static readonly string ErrorTextExistingSPNeedsDBName = "The stored procedure '{0}' is marked as existing and must have a defined DatabaseObjectName";
        public static readonly string ErrorTextRelationM_NRoleMismatch = "The role names for the two incomming relations for the associative table '{0}' must match";
        public static readonly string ErrorTextRelationM_NNameDuplication = "Two associative entities are defined between the same outer entities '{0}' and '{1}' with the same role name. You must use a unique role name for the relations.";
        public static readonly string ErrorTextTableAssociativeNoAuditTracking = "The associative entity '{0}' cannot have audit tracking enabled.";
        public static readonly string ErrorTextTableAssociativeNoCRUDAudit = "The associative entity '{0}' cannot have audit tracking enabled (created, modified, timestamp) when 'StoredProceduresForCRUD' is false.";
        public static readonly string ErrorTextTableBadStaticData = "One or more static data entries cannot be converted to specified data type.";
        public static readonly string ErrorTextDecimalColumnTooSmall = "The decimal parameter '{0}' has a very small length of {1}. This may be a mistake.";
        public static readonly string ErrorTextRelationCausesNameConflict = "A M:N relation for entity '{0}' causes a naming conflict in another relation with entity '{1}'";
        public static readonly string ErrorTextRelationDuplicate = "There are 2 or more relations on the entity '{0}' to the same target entity on the same fields.";
        public static readonly string ErrorTextRelationNeedUniqueFields = "The relation {0}->{1} is invalid. All source and target fields within a relation must be unqiue.";
        public static readonly string ErrorTextMetadataInvalid = "Metadata must have a unique, non-null key";
        public static readonly string ErrorTextInvalidStoredProcPrefix = "The stored procedure prefix must be set";
        public static readonly string ErrorTextTableAssociativeNeedsNonOverlappingColumns = "The associative table '{0}' must contain the primary keys of the parent entities with no overlapping columns.";
        public static readonly string ErrorTextTableAssociativeNeedsOnlyPK = "The associative table '{0}' can only contain primary key fields.";
        public static readonly string WarningTextNeedParameters = "The object '{0}' may require parameters to work properly.";
        public static readonly string WarningTextSelectCommandNeedParameters = "The object '{0}' has a select command {1} that may require parameters to work properly.";
        public static readonly string ErrorTextNoModules = "The must be one or more modules.";
        public static readonly string ErrorTextModulesNotUnique = "All modules must have unqiue names.";
        public static readonly string ErrorTextModuleEntityFieldMismatch = "The field '{0}' is in module '{1}' but its parent entity is not.";
        public static readonly string ErrorTextModuleItemNotInModule = "The object '{0}' is not in any module.";
        public static readonly string ErrorTextModuleEntityPKMismatch = "The entity '{0}' is in module '{1}' so one or more of its primary key fields must be as well.";
        public static readonly string WarningTextModuleEntityPKMismatch = "The entity '{0}' is in module '{1}' but some of its primary keys are not.";
        public static readonly string ErrorTextFunctionZeroFields = "The function '{0}' must have one or more fields.";
        public static readonly string ErrorTextFunctionScalerMultipleFields = "The scaler function '{0}' must have exactly one field.";
        public static readonly string WarningTextRelationNotInModule = "This relation is not in any module.";
        public static readonly string WarningTextRelationNotInModuleWithParentChildTables = "The parent entity '{0}' and child entity '{1}' are both in module '{2}' but a relation between them is not.";
        public static readonly string WarningErrorModuleRuleInvalid = "A rule for the module '{0}' does not have a dependent module.";
        public static readonly string WarningErrorModuleRuleLogicFail = "The module '{0}' has an {1} rule with dependent module '{2}' that fails. ";
        public static readonly string WarningErrorModuleRuleNoDependentModule = "The module '{0}' has an {1} rule with no dependent module.";
        public static readonly string ErrorTextRelationFieldDuplicated = "The relation {0}->{1} cannot have duplicate columns between the parent and child entities.";
        public static readonly string ErrorTextRelationshipMustHaveFields = "The relation {0}->{1} must have one or fields defined.";
        public static readonly string ErrorTextModuleIsEmpty = "The module '{0}' must have at least one generated entity in it.";
        public static readonly string ErrorTextColumnMaxLengthViolation = "The defined length of item '{0}' is greater than the maximum length of {1} for the {2} data type.";
        public static readonly string ErrorTextColumnIdentityHasDefault = "The identity field {0} cannot have a default.";
        public static readonly string ErrorTextFunctionReturnVarNotValid = "The function return variable '{0}' for function '{1}' is not a valid identifier.";
        public static readonly string ErrorTextFunctionReturnVarForTabelFunc = "The function '{0}' cannot define a return variable because it is not a table-valued function.";
        public static readonly string ErrorTextColumnReadonlyNeedsDefault = "The object '{0}' is marked read-only and not nullable, so it must have a default value.";
        public static readonly string ErrorTextRelationNoFields = "The relationship '{0}' must have at least one link field.";
        public static readonly string ErrorTextEntityIndexInvalid = "One or more of the indexes for '{0}' Entity is invalid.";
        public static readonly string ErrorTextEntityIndexInvalidLength = "The index has a field '{0}' that has an invalid length.";
        public static readonly string ErrorTextEntityIndexIsPossibleDuplicate = "The Entity '{0}' has one or more indexes that are possible duplicates. They have the same indexed columns in different order.";
        public static readonly string ErrorTextEntityIndexIsDuplicate = "The Entity '{0}' has one or more indexes that are duplicated.";
        public static readonly string ErrorTextEntityIndexMultipleClustered = "The Entity '{0}' has more than one clustered index.";
        public static readonly string ErrorTextRelationshipMustBeInModule = "The Entity '{0}' has a relationship with '{1}' that is not in any module.";
        public static readonly string ErrorTextInheritFieldOnly1Identity = "The primary key '{1}' for Entity '{0}' has more than one identity defined in the inheritance hierarchy.";
        public static readonly string ErrorTextInheritTopMostMustBeIndentity = "The primary key field '{1}' for Entity '{0}' cannot be an identity. A field identity can only be set on the top level field in table '{2}'.";
        public static readonly string ErrorTextTableInheritMustContainParentInModule = "The ancestor entity '{1}' must be in all modules with the child entity '{0}'.";
        public static readonly string ErrorTextDuplicateParameters = "The object {0} has duplicate parameter names.";
        public static readonly string ErrorTextOutputTargetInvalid = "The OutputTarget is not a valid format for a solution folder.";
        public static readonly string ErrorTextOnlyOneTimestamp = "The entity '{0}' may only have one timestamp field including the AllowTimestamp setting.";
        public static readonly string ErrorTextTenantTypeTable = "Database typed tables cannot also be tenant tables.";
        public static readonly string ErrorTextTenantTypeTableTenantColumnMatch = "The tenant table '{0}' cannot have an explicitly defined field named '{1}'";
        public static readonly string ErrorTextStoredProcNoColumns = "The stored procedure '{0}' must have at least one field.";
        public static readonly string ErrorTextClusteredGuid = "The UniqueIdentifier field '{0}' on entity '{1}' is in a clustered index. This may impact performance.";
        public static readonly string ErrorTextNullableFieldHasDefault = "The field '{0}' in entity '{1}' is a nullable field but has a default. The field will never by null.";
        public static readonly string ErrorTextNullableFieldHasDefaultWithRelation = "The field '{0}' in entity '{1}' is a nullable field in a relationship as the foreign key with entity '{2}'. A default is not allowed as it may cause foreign key violations.";
        public static readonly string ErrorTextModuleContainIndexNotFields = "The entity '{0}' has an index with fields [{1}] in module '{2}' but one or more of the fields is not in the module.";
        public static readonly string ErrorTextIndexNotInModule = "The entity '{0}' has index [{1}] that must be in a module.";
        public static readonly string ErrorTextVersionNegative = "The major version number cannot be negative.";
        public static readonly string ErrorTextDateTimeDeprecated = "The field '{0}' is marked DateTime. In SQL 2008 and above DateTime2 provides better functionality.";
        public static readonly string ErrorTextSecurityFunction = "The security function for entity '{0}' is not valid.";
        public static readonly string ErrorTextColumnNoRange4Identity = "The identity field {0} cannot have a Min/Max value.";
        public static readonly string ErrorTextGeneratedCRUD_EF4 = "Generated CRUD operations are only supported for EF4 projects.";
        public static readonly string ErrorTextFKNeedIndex = "The field {0} is a foreign key but there is no index on this field.";

        public const string ValidCodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
        //private const string reservedSQLWords = "ENCRYPTION,ORDER,ADD,END,OUTER,ALL,ERRLVL,OVER,ALTER,ESCAPE,PERCENT,AND,EXCEPT,PLAN,ANY,EXEC,PRECISION,AS,EXECUTE,PRIMARY,ASC,EXISTS,PRINT,AUTHORIZATION,EXIT,PROC,AVG,EXPRESSION,PROCEDURE,BACKUP,FETCH,PUBLIC,BEGIN,FILE,RAISERROR,BETWEEN,FILLFACTOR,READ,BREAK,FOR,READTEXT,BROWSE,FOREIGN,RECONFIGURE,BULK,FREETEXT,REFERENCES,BY,FREETEXTTABLE,REPLICATION,CASCADE,FROM,RESTORE,CASE,FULL,RESTRICT,CHECK,RETURN,CHECKPOINT,GOTO,REVOKE,CLOSE,GRANT,RIGHT,CLUSTERED,GROUP,ROLLBACK,COALESCE,HAVING,ROWCOUNT,COLLATE,HOLDLOCK,ROWGUIDCOL,COLUMN,IDENTITY,RULE,COMMIT,IDENTITY_INSERT,SAVE,COMPUTE,IDENTITYCOL,SCHEMA,CONSTRAINT,IF,SELECT,CONTAINS,IN,SESSION_USER,CONTAINSTABLE,INDEX,SET,CONTINUE,INNER,SETUSER,CONVERT,INSERT,SHUTDOWN,COUNT,INTERSECT,SOME,CREATE,INTO,STATISTICS,CROSS,IS,SUM,CURRENT,JOIN,SYSTEM_USER,CURRENT_DATE,KEY,TABLE,CURRENT_TIME,KILL,TEXTSIZE,CURRENT_TIMESTAMP,LEFT,THEN,CURRENT_USER,LIKE,TO,CURSOR,LINENO,TOP,DATABASE,LOAD,TRAN,DATABASEPASSWORD,MAX,TRANSACTION,DATEADD,MIN,TRIGGER,DATEDIFF,NATIONAL,TRUNCATE,DATENAME,NOCHECK,TSEQUAL,DATEPART,NONCLUSTERED,UNION,DBCC,NOT,UNIQUE,DEALLOCATE,NULL,UPDATE,DECLARE,NULLIF,UPDATETEXT,DEFAULT,OF,USE,DELETE,OFF,USER,DENY,OFFSETS,VALUES,DESC,ON,VARYING,DISK,OPEN,VIEW,DISTINCT,OPENDATASOURCE,WAITFOR,DISTRIBUTED,OPENQUERY,WHEN,DOUBLE,OPENROWSET,WHERE,DROP,OPENXML,WHILE,DUMP,OPTION,WITH,ELSE,OR,WRITETEXT";
        private const string reservedSQLWords = "";
        //REMOVED: region,Property
        //private const string reservedCSharpWords = "_Bool,_Complex,_Imaginary,_Packed,abstract,as,auto,base,bool,break,byte,case,catch,char,checked,class,const,continue,decimal,default,delegate,do,double,else,enum,event,exception,explicit,extern,false,finally,fixed,float,for,foreach,goto,if,implicit,in,inline,int,interface,internal,is,lock,long,namespace,new,null,object,operator,out,override,params,private,protected,public,readonly,ref,register,restrict,return,sbyte,sealed,short,signed,sizeof,stackalloc,static,string,struct,switch,this,throw,true,try,typedef,typeof,uint,ulong,unchecked,union,unsafe,unsigned,ushort,using,virtual,void,volatile,while,PrimaryKey,attribute,businessobject,attribute,system,BusinessCollectionBase,BusinessCollectionPersistableBase,BusinessObjectBase,BusinessObjectList,BusinessObjectPersistableBase,IVisitee,IVisitor,IAuditable,IPagingObject,IPagingFieldItem,DomainCollectionBase,Enumerations,IBusinessCollection,IBusinessObject,IBusinessObjectSearch,IDomainCollection,IDomainObject,IErrorRow,IPersistableBusinessCollection,IPersistableBusinessObject,IPersistableDomainObject,IPrimaryKey,IPropertyBag,IPropertyDefine,IReadOnlyBusinessCollection,IReadOnlyBusinessObject,ITyped,IWrappingClass,PersistableDomainCollectionBase,PersistableDomainObjectBase,PrimaryKey,ReadOnlyDomainCollection,ReadOnlyDomainObjectBase,SelectCommand,StoredProcedure,StoredProcedureFactory,SubDomainBase,SubDomainWrapper,BusinessObjectCancelEventArgs,BusinessObjectEventArgs,ConcurrencyException,DefaultLogConfiguration,ILogClass,ILogConfiguration,MultiProcessTraceListener,TraceSwitchAttribute,IXmlable,IBoolFieldDescriptor,IDateFieldDescriptor,IFieldDescriptor,IFloatFieldDescriptor,IIntegerFieldDescriptor,IStringFieldDescriptor,As,Assembly,Auto,Base,Boolean,ByRef,Byte,ByVal,Call,Case,Catch,CBool,CByte,CChar,CDate,CDec,CDbl,Char,CInt,Class,CLng,CObj,Const,CShort,CSng,CStr,CType,Date,Decimal,Declare,Default,Delegate,Dim,Do,Double,Each,Else,ElseIf,End,Enum,Erase,Error,Event,Exit,ExternalSource,False,Finalize,Finally,Float,For,Friend,Get,GetType,Goto,Handles,If,Implements,Imports,In,Inherits,Integer,Interface,Is,Let,Lib,Like,Long,Loop,Me,Mod,MustInherit,MustOverride,MyBase,MyClass,Namespace,New,Next,Not,Nothing,NotInheritable,NotOverridable,Object,On,Option,Optional,Or,Overloads,Overridable,Overrides,ParamArray,Preserve,Private,Protected,Public,RaiseEvent,ReadOnly,ReDim,REM,RemoveHandler,Resume,Return,Select,Set,Shadows,Shared,Short,Single,Static,Step,Stop,String,Structure,Sub,SyncLock,Then,Throw,To,True,Try,TypeOf,Unicode,Until,volatile,When,While,With,WithEvents,WriteOnly,Xor,eval,extends,instanceof,package,var";
        private const string reservedCSharpWords = "_Bool,_Complex,_Imaginary,_Packed,abstract,as,auto,base,bool,break,byte,case,catch,char,checked,class,const,continue,decimal,default,delegate,do,double,else,enum,event,exception,explicit,extern,false,finally,fixed,float,for,foreach,goto,if,implicit,in,inline,int,interface,internal,is,lock,long,namespace,new,null,object,operator,out,override,params,private,protected,public,readonly,ref,register,restrict,return,sbyte,sealed,short,signed,sizeof,stackalloc,static,string,struct,switch,this,throw,true,try,typedef,typeof,uint,ulong,unchecked,union,unsafe,unsigned,ushort,using,virtual,void,volatile,while,PrimaryKey,attribute,businessobject,attribute,system,BusinessCollectionBase,BusinessCollectionPersistableBase,BusinessObjectBase,BusinessObjectList,BusinessObjectPersistableBase,IVisitee,IVisitor,IAuditable,IPagingObject,IPagingFieldItem,DomainCollectionBase,Enumerations,IBusinessCollection,IBusinessObject,IBusinessObjectSearch,IDomainCollection,IDomainObject,IErrorRow,IPersistableBusinessCollection,IPersistableBusinessObject,IPersistableDomainObject,IPrimaryKey,IPropertyBag,IPropertyDefine,IReadOnlyBusinessCollection,IReadOnlyBusinessObject,ITyped,IWrappingClass,PersistableDomainCollectionBase,PersistableDomainObjectBase,PrimaryKey,ReadOnlyDomainCollection,ReadOnlyDomainObjectBase,SelectCommand,StoredProcedure,StoredProcedureFactory,SubDomainBase,SubDomainWrapper,BusinessObjectCancelEventArgs,BusinessObjectEventArgs,ConcurrencyException,DefaultLogConfiguration,ILogClass,ILogConfiguration,MultiProcessTraceListener,TraceSwitchAttribute,IXmlable,IBoolFieldDescriptor,IDateFieldDescriptor,IFieldDescriptor,IFloatFieldDescriptor,IIntegerFieldDescriptor,IStringFieldDescriptor,As,Assembly,Auto,Base,Boolean,ByRef,Byte,ByVal,Call,Case,Catch,CBool,CByte,CChar,CDate,CDec,CDbl,Char,CInt,Class,CLng,CObj,Const,CShort,CSng,CStr,CType,Date,Decimal,Declare,Default,Delegate,Dim,Do,Double,Enum,Erase,Error,Event,Exit,ExternalSource,False,Finalize,Finally,Float,For,Friend,Get,GetType,Handles,If,Implements,Imports,In,Inherits,Integer,Interface,Is,Let,Lib,Like,Long,Loop,Me,Mod,Namespace,New,Next,Not,Nothing,Object,On,Option,Optional,Or,Preserve,Private,Protected,Public,RaiseEvent,ReadOnly,ReDim,REM,RemoveHandler,Return,Select,Set,Shadows,Shared,Short,Single,Static,Step,Stop,String,Structure,Sub,SyncLock,Then,Throw,To,True,Try,TypeOf,Unicode,Until,volatile,When,While,With,WithEvents,WriteOnly,Xor,eval,extends,instanceof,package,var";
        private const string reservedFields = "clone,isparented,container,delete,equals,getdatetime,getdefault,getdouble,getfieldlength,GetFriendlyName,GetHashCode,GetInteger,GetString,GetValue,IsEquivalent,ItemState,OnValidate,ParentCollection,Persist,RejectChanges,PrimaryKey,ReleaseNonIdentifyingRelationships,Remove,SelectUsingPK,SetCreatedDate,SetModifiedDate,SetValue,Validate,wrappedClass,DeleteData,GetDatabaseFieldName,GetFieldAliasFromFieldNameSqlMapping,GetMaxLength,GetPagedSQL,GetRemappedLinqSql,GetTableFromFieldAliasSqlMapping,GetTableFromFieldNameSqlMapping,UpdateData,isvalid";
        private const string reservedEntityNames = "version";

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
            var validchars2 = ValidCodeChars + " /#@$_.";

            foreach (var c in name)
            {
                if (validchars2.IndexOf(c) == -1)
                    return false;
            }

            //EF does NOT allow underscore as the first character
            if (name.First() == '_')
                return false;

            return true;
        }

        /// <summary>
        /// Determines if the name is a reserved word
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsReservedWord(string name)
        {
            var words = reservedCSharpWords.ToLower().Split(',');

            var q = (from x in words
                     where name.ToLower() == x
                     select x).FirstOrDefault();

            return (q != null);
        }

        public static bool ValidEntityName(string name)
        {
            if (!ValidCodeIdentifier(name))
                return false;

            var words = reservedEntityNames.ToLower().Split(',');

            var q = (from x in words
                     where name.ToLower() == x
                     select x).FirstOrDefault();

            return (q == null);
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
            return MakeCodeIdentifer(name, "_");
        }

        public static string MakeCodeIdentifer(string name, string invalidReplacement)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            //If this is a reserved word then add "Value" to the end
            if (IsReservedWord(name))
            {
                name += "Value";
            }

            var retval = string.Empty;
            var validchars2 = ValidCodeChars;

            foreach (var c in name)
            {
                if (validchars2.IndexOf(c) == -1)
                    retval += invalidReplacement;
                else
                    retval += c;
            }
            return retval;
        }

        public static string MakeDatabaseIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var retval = string.Empty;
            var validchars2 = ValidCodeChars + " /#";

            foreach (var c in name)
            {
                if (validchars2.IndexOf(c) == -1)
                    retval += '_';
                else
                    retval += c;
            }
            return retval;
        }

        public static string MakeDatabaseScriptIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var retval = string.Empty;
            foreach (var c in name)
            {
                if (ValidCodeChars.IndexOf(c) == -1)
                    retval += '_';
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