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
using System.Linq;
using nHydrate.Generator.Common.GeneratorFramework;
using nHydrate.Generator.Models;
using System.Text;
using nHydrate.Generator.Common.Util;
using System.Collections.Generic;

namespace nHydrate.Generator.EFCodeFirst.Generators.ContextExtensions
{
    public class ContextExtensionsGeneratedTemplate : EFCodeFirstBaseTemplate
    {
        private StringBuilder sb = new StringBuilder();

        public ContextExtensionsGeneratedTemplate(ModelRoot model)
            : base(model)
        {
        }

        #region BaseClassTemplate overrides
        public override string FileName
        {
            get { return _model.ProjectName + "EntitiesExtensions.Generated.cs"; }
        }

        public string ParentItemName
        {
            get { return _model.ProjectName + "EntitiesExtensions.cs"; }
        }

        public override string FileContent
        {
            get
            {
                GenerateContent();
                return sb.ToString();
            }
        }
        #endregion

        #region GenerateContent

        private void GenerateContent()
        {
            try
            {
                nHydrate.Generator.GenerationHelper.AppendFileGeneatedMessageInCode(sb);
                nHydrate.Generator.GenerationHelper.AppendCopyrightInCode(sb, _model);
                this.AppendUsingStatements();
                sb.AppendLine("namespace " + this.GetLocalNamespace());
                sb.AppendLine("{");
                this.AppendExtensions();
                sb.AppendLine("}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AppendUsingStatements()
        {
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine($"using {this.GetLocalNamespace()}.Entity;");
            sb.AppendLine("using System.Linq.Expressions;");
            sb.AppendLine();
        }

        private void AppendExtensions()
        {
            sb.AppendLine("	#region " + _model.ProjectName + "EntitiesExtensions");
            sb.AppendLine();
            sb.AppendLine("	/// <summary>");
            sb.AppendLine("	/// Extension methods for this library");
            sb.AppendLine("	/// </summary>");
            sb.AppendLine($"	[System.CodeDom.Compiler.GeneratedCode(\"nHydrateModelGenerator\", \"{_model.ModelToolVersion}\")]");
            sb.AppendLine("	public static partial class " + _model.ProjectName + "EntitiesExtensions");
            sb.AppendLine("	{");

            #region Include Extension Methods
            //Add an strongly-typed extension for "Include" method
            sb.AppendLine("		#region Include Extension Methods");
            sb.AppendLine();

            sb.AppendLine("		private static System.Data.Entity.Infrastructure.DbQuery<T> GetInclude<T, R>(this System.Data.Entity.Infrastructure.DbQuery<T> item, Expression<Func<R, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
            sb.AppendLine("			where T : BaseEntity");
            sb.AppendLine("			where R : IContextInclude");
            sb.AppendLine("		{");
            sb.AppendLine("			var builder = new List<string>();");
            sb.AppendLine("			var zz = query.Body as MemberExpression;");
            sb.AppendLine("			while (zz != null)");
            sb.AppendLine("			{");
            sb.AppendLine("				var tt = zz.Member.GetCustomAttributes(typeof(EntityMap), true).FirstOrDefault() as EntityMap;");
            sb.AppendLine("				if (tt != null) builder.Add(tt.Name);");
            sb.AppendLine("				else builder.Add(zz.Member.Name);");
            sb.AppendLine("				zz = zz.Expression as MemberExpression;");
            sb.AppendLine("			}");
            sb.AppendLine("			builder.Reverse();");
            sb.AppendLine();
            sb.AppendLine("			var compoundString = string.Empty;");
            sb.AppendLine("			foreach (var s in builder)");
            sb.AppendLine("			{");
            sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            sb.AppendLine("				compoundString += s;");
            sb.AppendLine("				item = item.Include(compoundString);");
            sb.AppendLine("			}");
            sb.AppendLine("			return item;");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		private static IQueryable<T> GetInclude<T, R>(this IQueryable<T> item, Expression<Func<R, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
            sb.AppendLine("			where T : BaseEntity");
            sb.AppendLine("			where R : IContextInclude");
            sb.AppendLine("		{");
            sb.AppendLine("			var dbItem = item as System.Data.Entity.Infrastructure.DbQuery<T>;");
            sb.AppendLine("			if (dbItem != null) return GetInclude(dbItem, query);");
            sb.AppendLine("			var tempItem = item as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            sb.AppendLine("			if (tempItem == null) return item;");
            sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            sb.AppendLine("			strings.RemoveAt(0);");
            sb.AppendLine("			var compoundString = string.Empty;");
            sb.AppendLine("			foreach (var s in strings)");
            sb.AppendLine("			{");
            sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            sb.AppendLine("				compoundString += s;");
            sb.AppendLine("				tempItem = tempItem.Include(compoundString);");
            sb.AppendLine("			}");
            sb.AppendLine("			return tempItem;");
            sb.AppendLine("		}");
            sb.AppendLine();

            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable == TypedTableConstants.None)).OrderBy(x => x.PascalName))
            {
                //Build relation list
                var relationList1 = table.GetRelationsFullHierarchy().Where(x =>
                    (x.ParentTableRef.Object == table ||
                    table.GetParentTables().Contains(x.ParentTableRef.Object)) &&
                    !(x.ChildTableRef.Object as Table).IsInheritedFrom(x.ParentTableRef.Object as Table)
                    ).ToList();

                var relationList2 = table.GetRelationsWhereChild().Where(x =>
                    x.ChildTableRef.Object == table &&
                    !(x.ChildTableRef.Object as Table).IsInheritedFrom(x.ParentTableRef.Object as Table)
                    ).ToList();

                var relationList = new List<Relation>();
                relationList.AddRange(relationList1);
                relationList.AddRange(relationList2);

                //Generate an extension if there are relations for this table
                if (relationList.Count() != 0 && table.TypedTable != TypedTableConstants.EnumOnly)
                {
                    //Add one for DbQuery
                    sb.AppendLine("		/// <summary>");
                    sb.AppendLine("		/// Specifies the related objects to include in the query results.");
                    sb.AppendLine("		/// </summary>");
                    sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
                    sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
                    sb.AppendLine("		public static System.Data.Entity.Infrastructure.DbQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> Include(this System.Data.Entity.Infrastructure.DbQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> item, Expression<Func<" + this.GetLocalNamespace() + "." + table.PascalName + "Include, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
                    sb.AppendLine("		{");
                    sb.AppendLine("			return GetInclude<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", " + this.GetLocalNamespace() + "." + table.PascalName + "Include>(item, query);");
                    sb.AppendLine("		}");
                    sb.AppendLine();

                    //Now add one for IQueryable
                    sb.AppendLine("		/// <summary>");
                    sb.AppendLine("		/// Specifies the related objects to include in the query results.");
                    sb.AppendLine("		/// </summary>");
                    sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
                    sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
                    sb.AppendLine("		public static IQueryable<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> Include(this IQueryable<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> item, Expression<Func<" + this.GetLocalNamespace() + "." + table.PascalName + "Include, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
                    sb.AppendLine("		{");
                    sb.AppendLine("			return GetInclude<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", " + this.GetLocalNamespace() + "." + table.PascalName + "Include>(item, query);");
                    sb.AppendLine("		}");
                    sb.AppendLine();

                }
            }
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region GetFieldType
            sb.AppendLine("		#region GetFieldType Extension Method");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Get the system type of a field of one of the contained context objects");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static System.Type GetFieldType(this " + this.GetLocalNamespace() + "." + _model.ProjectName + "Entities context, Enum field)");
            sb.AppendLine("		{");
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable != TypedTableConstants.EnumOnly)).OrderBy(x => x.PascalName))
            {
                sb.AppendLine("			if (field is " + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)");
                sb.AppendLine("				return " + GetLocalNamespace() + ".Entity." + table.PascalName + ".GetFieldType((" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)field);");
            }
            sb.AppendLine("			throw new Exception(\"Unknown field type!\");");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region Metadata Extension Methods
            sb.AppendLine("		#region Metadata Extension Methods");
            sb.AppendLine();

            //Main one for base IReadOnlyBusinessObject object
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Creates and returns a metadata object for an entity type");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"entity\">The source class</param>");
            sb.AppendLine("		/// <returns>A metadata object for the entity types in this assembly</returns>");
            sb.AppendLine("		public static " + this.GetLocalNamespace() + ".IMetadata GetMetaData(this " + this.GetLocalNamespace() + ".IReadOnlyBusinessObject entity)");
            sb.AppendLine("		{");
            sb.AppendLine("			var a = entity.GetType().GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.MetadataTypeAttribute), true).FirstOrDefault();");
            sb.AppendLine("			if (a == null) return null;");
            sb.AppendLine("			var t = ((System.ComponentModel.DataAnnotations.MetadataTypeAttribute)a).MetadataClassType;");
            sb.AppendLine("			if (t == null) return null;");
            sb.AppendLine("			return Activator.CreateInstance(t) as " + this.GetLocalNamespace() + ".IMetadata;");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region GetEntityType

            sb.AppendLine("		#region GetEntityType");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Determines the entity from one of its fields");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static System.Type GetEntityType(EntityMappingConstants entityType)");
            sb.AppendLine("		{");
            sb.AppendLine("			switch (entityType)");
            sb.AppendLine("			{");
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable != TypedTableConstants.EnumOnly)).OrderBy(x => x.PascalName))
                sb.AppendLine("				case EntityMappingConstants." + table.PascalName + ": return typeof(" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ");");
            sb.AppendLine("			}");
            sb.AppendLine("			throw new Exception(\"Unknown entity type!\");");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		#endregion");
            sb.AppendLine();

            #endregion

            #region GetValue Methods
            sb.AppendLine("		#region GetValue Methods");
            sb.AppendLine();

            //GetValue by lambda
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Gets the value of one of this object's properties.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
            sb.AppendLine("		/// <typeparam name=\"R\">The type of object from which retrieve the field value</typeparam>");
            sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
            sb.AppendLine("		/// <param name=\"selector\">The field to retrieve</param>");
            sb.AppendLine("		/// <returns></returns>");
            sb.AppendLine("		public static T GetValue<T, R>(this R item, System.Linq.Expressions.Expression<System.Func<R, T>> selector)");
            sb.AppendLine("			where R : BaseEntity");
            sb.AppendLine("		{");
            sb.AppendLine("			var b = selector.Body.ToString();");
            sb.AppendLine("			var arr = b.Split('.');");
            sb.AppendLine("			if (arr.Length != 2) throw new System.Exception(\"Invalid selector\");");
            sb.AppendLine("			var tn = arr.Last();");
            sb.AppendLine("			var ft = ((IReadOnlyBusinessObject)item).GetFieldNameConstants();");
            sb.AppendLine("			var te = (System.Enum)Enum.Parse(ft, tn, true);");
            sb.AppendLine("			return item.GetValueInternal<T, R>(field: te, defaultValue: default(T));");
            sb.AppendLine("		}");
            sb.AppendLine();

            //GetValue by lambda with default
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Gets the value of one of this object's properties.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
            sb.AppendLine("		/// <typeparam name=\"R\">The type of object from which retrieve the field value</typeparam>");
            sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
            sb.AppendLine("		/// <param name=\"selector\">The field to retrieve</param>");
            sb.AppendLine("		/// <param name=\"defaultValue\">The default value to return if the specified value is NULL</param>");
            sb.AppendLine("		/// <returns></returns>");
            sb.AppendLine("		public static T GetValue<T, R>(this R item, System.Linq.Expressions.Expression<System.Func<R, T>> selector, T defaultValue)");
            sb.AppendLine("			where R : BaseEntity");
            sb.AppendLine("		{");
            sb.AppendLine("			var b = selector.Body.ToString();");
            sb.AppendLine("			var arr = b.Split('.');");
            sb.AppendLine("			if (arr.Length != 2) throw new System.Exception(\"Invalid selector\");");
            sb.AppendLine("			var tn = arr.Last();");
            sb.AppendLine("			var ft = ((IReadOnlyBusinessObject)item).GetFieldNameConstants();");
            sb.AppendLine("			var te = (System.Enum)Enum.Parse(ft, tn, true);");
            sb.AppendLine("			return item.GetValueInternal<T, R>(field: te, defaultValue: defaultValue);");
            sb.AppendLine("		}");
            sb.AppendLine();

            //GetValue by by Enum with default
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Gets the value of one of this object's properties.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
            sb.AppendLine("		/// <typeparam name=\"R\">The type of object from which retrieve the field value</typeparam>");
            sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
            sb.AppendLine("		/// <param name=\"field\">The field value to retrieve</param>");
            sb.AppendLine("		/// <param name=\"defaultValue\">The default value to return if the specified value is NULL</param>");
            sb.AppendLine("		/// <returns></returns>");
            sb.AppendLine("		private static T GetValueInternal<T, R>(this R item, System.Enum field, T defaultValue)");
            sb.AppendLine("			where R : BaseEntity");
            sb.AppendLine("		{");
            sb.AppendLine("			var valid = false;");
            sb.AppendLine("			if (typeof(T) == typeof(bool)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(byte)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(char)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(DateTime)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(decimal)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(double)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(int)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(long)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(Single)) valid = true;");
            sb.AppendLine("			else if (typeof(T) == typeof(string)) valid = true;");
            sb.AppendLine("			if (!valid)");
            sb.AppendLine("				throw new Exception(\"Cannot convert object to type '\" + typeof(T).ToString() + \"'!\");");
            sb.AppendLine();
            sb.AppendLine("			object o = ((IReadOnlyBusinessObject)item).GetValue(field, defaultValue);");
            sb.AppendLine("			if (o == null) return defaultValue;");
            sb.AppendLine();
            sb.AppendLine("			if (o is T)");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)o;");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(bool))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToBoolean(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(byte))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToByte(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(char))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToChar(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(DateTime))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToDateTime(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(decimal))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToDecimal(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(double))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToDouble(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(int))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToInt32(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(long))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToInt64(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(Single))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToSingle(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			else if (typeof(T) == typeof(string))");
            sb.AppendLine("			{");
            sb.AppendLine("				return (T)(object)Convert.ToString(o);");
            sb.AppendLine("			}");
            sb.AppendLine("			throw new Exception(\"Cannot convert object!\");");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region SetValue
            sb.AppendLine("		#region SetValue");

            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Assigns a value to a field on this object.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"item\">The entity to set</param>");
            sb.AppendLine("		/// <param name=\"selector\">The field on the entity to set</param>");
            sb.AppendLine("		/// <param name=\"newValue\">The new value to assign to the field</param>");
            sb.AppendLine("		public static void SetValue<TResult, R>(this R item, System.Linq.Expressions.Expression<System.Func<R, TResult>> selector, TResult newValue)");
            sb.AppendLine("			where R : BaseEntity, IBusinessObject");
            sb.AppendLine("		{");
            sb.AppendLine("			SetValue(item: item, selector: selector, newValue: newValue, fixLength: false);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Assigns a value to a field on this object.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"item\">The entity to set</param>");
            sb.AppendLine("		/// <param name=\"selector\">The field on the entity to set</param>");
            sb.AppendLine("		/// <param name=\"newValue\">The new value to assign to the field</param>");
            sb.AppendLine("		/// <param name=\"fixLength\">Determines if the length should be truncated if too long. When false, an error will be raised if data is too large to be assigned to the field.</param>");
            sb.AppendLine("		public static void SetValue<TResult, R>(this R item, System.Linq.Expressions.Expression<System.Func<R, TResult>> selector, TResult newValue, bool fixLength)");
            sb.AppendLine("			where R : BaseEntity, IBusinessObject");
            sb.AppendLine("		{");
            sb.AppendLine("			var b = selector.Body.ToString();");
            sb.AppendLine("			var arr = b.Split('.');");
            sb.AppendLine("			if (arr.Length != 2) throw new System.Exception(\"Invalid selector\");");
            sb.AppendLine("			var tn = arr.Last();");
            sb.AppendLine("			var ft = ((IReadOnlyBusinessObject)item).GetFieldNameConstants();");
            sb.AppendLine("			var te = (System.Enum)Enum.Parse(ft, tn, true);");
            sb.AppendLine("			((IBusinessObject)item).SetValue(field: te, newValue: newValue, fixLength: fixLength);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region ObservableCollection
            sb.AppendLine("		#region ObservableCollection");
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Returns an observable collection that can bound to UI controls");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static System.Collections.ObjectModel.ObservableCollection<T> AsObservable<T>(this System.Collections.Generic.IEnumerable<T> list)");
            sb.AppendLine("			where T : " + this.GetLocalNamespace() + ".IReadOnlyBusinessObject");
            sb.AppendLine("		{");
            sb.AppendLine("			var retval = new System.Collections.ObjectModel.ObservableCollection<T>();");
            sb.AppendLine("			foreach (var o in list)");
            sb.AppendLine("				retval.Add(o);");
            sb.AppendLine("			return retval;");
            sb.AppendLine("		}");
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region Delete Extensions
            sb.AppendLine("		#region Delete Extensions");
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Delete all records that match a where condition");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static void Delete<T>(this IQueryable<T> query)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			query.Delete(optimizer: null, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Delete all records that match a where condition");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static void Delete<T>(this IQueryable<T> query, QueryOptimizer optimizer)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			query.Delete(optimizer: optimizer, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Delete all records that match a where condition");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static void Delete<T>(this IQueryable<T> query, string connectionString)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			query.Delete(optimizer: new QueryOptimizer(), connectionString: connectionString);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Delete all records that match a where condition");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static void  Delete<T>(this IQueryable<T> query, QueryOptimizer optimizer, string connectionString)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			if (optimizer == null)");
            sb.AppendLine("				optimizer = new QueryOptimizer();");
            sb.AppendLine("			if (query == null)");
            sb.AppendLine("				throw new Exception(\"Query must be set\");");
            sb.AppendLine();
            sb.AppendLine("			//There is nothing to do");
            sb.AppendLine("			if (query.ToString().Replace(\"\\r\", string.Empty).Split(new char[] { '\\n' }).LastOrDefault().Trim() == \"WHERE 1 = 0\")");
            sb.AppendLine("				return;");
            sb.AppendLine();
            sb.AppendLine("			var instanceKey = Guid.Empty;");
            sb.AppendLine("			System.Data.Entity.Core.Objects.ObjectContext objectContext = null;");
            sb.AppendLine("			try");
            sb.AppendLine("			{");
            sb.AppendLine();
            sb.AppendLine("				var propContext = query.Provider.GetType().GetProperty(\"InternalContext\");");
            sb.AppendLine("				if (propContext != null)");
            sb.AppendLine("				{");
            sb.AppendLine("					var context = propContext.GetValue(query.Provider);");
            sb.AppendLine("					if (context != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var oc = context.GetType().GetProperty(\"ObjectContext\").GetValue(context) as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						objectContext = oc as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						instanceKey = ((IContext)context.GetType().GetProperty(\"Owner\").GetValue(context)).InstanceKey;");
            sb.AppendLine("						if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("						{");
            sb.AppendLine("							var propCs = context.GetType().GetProperty(\"OriginalConnectionString\");");
            sb.AppendLine("							if (propCs != null) connectionString = (string)propCs.GetValue(context);");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine();
            sb.AppendLine("				if (instanceKey == Guid.Empty)");
            sb.AppendLine("				{");
            sb.AppendLine("					var context2 = query.Provider.GetType().GetField(\"_context\", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);");
            sb.AppendLine("					if (context2 != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var context = context2.GetValue(query.Provider);");
            sb.AppendLine("						objectContext = context as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						var qq = objectContext.InterceptionContext.DbContexts.First() as " + this.GetLocalNamespace() + ".I" + _model.ProjectName + "Entities;");
            sb.AppendLine("						if (qq != null)");
            sb.AppendLine("							instanceKey = qq.InstanceKey;");
            sb.AppendLine("						if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("						{");
            sb.AppendLine("							connectionString = Util.StripEFCS2Normal(objectContext.Connection.ConnectionString);");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine();
            sb.AppendLine("				if (instanceKey == Guid.Empty)");
            sb.AppendLine("					throw new Exception(\"Unknown context\");");
            sb.AppendLine();
            sb.AppendLine("				if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("				{");
            sb.AppendLine("					var propContext2 = query.GetType().GetProperty(\"Context\");");
            sb.AppendLine("					if (propContext2 != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var context = propContext2.GetValue(query) as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						if (context != null)");
            sb.AppendLine("						{");
            sb.AppendLine("							var builder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(context.Connection.ConnectionString);");
            sb.AppendLine("							if (!string.IsNullOrWhiteSpace(builder.ProviderConnectionString))");
            sb.AppendLine("							{");
            sb.AppendLine("								objectContext = context;");
            sb.AppendLine("								connectionString = builder.ProviderConnectionString;");
            sb.AppendLine("							}");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			catch { }");
            sb.AppendLine();

            sb.AppendLine("			System.Data.Entity.Core.Objects.ObjectParameterCollection existingParams = null;");
            sb.AppendLine("			{");
            sb.AppendLine("			    var objectQuery = query as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            sb.AppendLine("			    if (objectQuery == null)");
            sb.AppendLine("			    {");
            sb.AppendLine("			        var internalQueryField = query.GetType().GetProperty(\"InternalQuery\", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(query);");
            sb.AppendLine("			        if (internalQueryField != null)");
            sb.AppendLine("			            objectQuery = internalQueryField.GetType().GetProperty(\"ObjectQuery\").GetValue(internalQueryField) as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            sb.AppendLine("			    }");
            sb.AppendLine();
            sb.AppendLine("			    if (objectQuery != null)");
            sb.AppendLine("			    {");
            sb.AppendLine("			        var ss2 = objectQuery.ToTraceString(); //DO NOT REMOVE! must call this to init params");
            sb.AppendLine("			        existingParams = objectQuery.GetType().GetProperty(\"Parameters\").GetValue(objectQuery) as System.Data.Entity.Core.Objects.ObjectParameterCollection;");
            sb.AppendLine("			    }");
            sb.AppendLine("			}");
            sb.AppendLine();

            sb.AppendLine("			var sb = new System.Text.StringBuilder();");
            sb.AppendLine("			#region Per table code");

            var index = 0;
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable == TypedTableConstants.None)).OrderBy(x => x.PascalName))
            {
                var tableName = table.DatabaseName;
                if (table.IsTenant)
                    tableName = _model.TenantPrefix + "_" + table.DatabaseName;
                
                var innerQueryToString = "((IQueryable<" + GetLocalNamespace() + ".Entity." + table.PascalName + ">)query).Select(x => new { " + string.Join(", ", table.PrimaryKeyColumns.Select(x => "x." + x.PascalName).ToList()) + " }).ToString()";
                if (table.Security.IsValid())
                    innerQueryToString = "((System.Data.Entity.Core.Objects.ObjectQuery)(((System.Data.Entity.Core.Objects.ObjectQuery<" + GetLocalNamespace() + ".Entity." + table.PascalName + ">)query).Select(x => new { " + string.Join(", ", table.PrimaryKeyColumns.Select(x => "x." + x.PascalName).ToList()) + " }))).ToTraceString()";

                sb.AppendLine("			" + (index == 0 ? string.Empty : "else ") + "if (typeof(T) == typeof(" + GetLocalNamespace() + ".Entity." + table.PascalName + "))");
                sb.AppendLine("			{");
                if (table.ParentTable == null)
                {
                    //Single table no parent, so no special code, easy delete
                    sb.AppendLine($"				sb.AppendLine(\"set rowcount \" + optimizer.ChunkSize + \";\");");
                    sb.AppendLine($"				sb.AppendLine(\"delete [X] from [{table.GetSQLSchema()}].[{tableName}] [X] inner join (\");");
                    sb.AppendLine($"				sb.AppendLine({innerQueryToString});");
                    sb.AppendLine($"				sb.AppendLine(\") AS [Extent2]\");");
                    sb.AppendLine($"				sb.AppendLine(\"on " + string.Join(" AND ", table.PrimaryKeyColumns.Select(x => "[X].[" + x.Name + "] = [Extent2].[" + x.Name + "]").ToList()) + "\");");
                    sb.AppendLine($"				sb.AppendLine(\"select @@ROWCOUNT\");");
                }
                else
                {
                    //For parented tables the hierarchy must be deleted
                    var tableParams = string.Join(", ", table.PrimaryKeyColumns.Select(x => "[" + x.DatabaseName + "] " + x.DatabaseType));
                    sb.AppendLine($"				sb.AppendLine(\"create table #t ({tableParams})\");");
                    sb.AppendLine($"				sb.AppendLine(\"insert into #t\");");
                    sb.AppendLine($"				sb.AppendLine({innerQueryToString} + \";\");");
                    sb.AppendLine($"				var joinQuery = \"select "+ string.Join(", ", table.PrimaryKeyColumns.Select(x => "[" + x.DatabaseName + "]")) + " from #t\";");
                    sb.AppendLine($"				sb.AppendLine(\"set rowcount \" + optimizer.ChunkSize + \";\");");

                    var allTables = table.GetParentTablesFullHierarchy().ToList();
                    allTables.Insert(0, table);
                    foreach (var tt in allTables)
                    {
                        sb.AppendLine($"				sb.AppendLine(\"delete [X] from [{tt.GetSQLSchema()}].[{tt.DatabaseName}] [X] inner join (\");");
                        sb.AppendLine($"				sb.AppendLine(joinQuery);");
                        sb.AppendLine($"				sb.AppendLine(\") AS [Extent2]\");");
                        sb.AppendLine($"				sb.AppendLine(\"on " + string.Join(" AND ", tt.PrimaryKeyColumns.Select(x => "[X].[" + x.Name + "] = [Extent2].[" + x.Name + "]").ToList()) + "\");");
                        if (tt == table)
                            sb.AppendLine("				sb.AppendLine(\"select @@ROWCOUNT\");");
                    }

                    sb.AppendLine("				sb.AppendLine(\"drop table #t;\");");
                }
                sb.AppendLine("			}");
                index++;
            }
            sb.AppendLine("			else throw new Exception(\"Entity type not found\");");
            sb.AppendLine("			#endregion");

            sb.AppendLine("			if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("				connectionString = " + _model.ProjectName + "Entities.GetConnectionString();");
            sb.AppendLine();
            sb.AppendLine("			var newParams = new List<System.Data.SqlClient.SqlParameter>();");
            sb.AppendLine("			if (existingParams != null)");
            sb.AppendLine("			{");
            sb.AppendLine("				foreach (var ep in existingParams)");
            sb.AppendLine("				{");
            sb.AppendLine("					newParams.Add(new System.Data.SqlClient.SqlParameter { ParameterName = ep.Name, Value = (ep.Value == null ? System.DBNull.Value : ep.Value) });");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			QueryPreCache.AddDelete(instanceKey, sb.ToString(), newParams, optimizer);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Delete<T>(this System.Data.Entity.DbSet<T> entitySet, Expression<Func<T, bool>> where)");
            sb.AppendLine("			where T : class, " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			entitySet.Where(where).Delete(optimizer: null, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Delete<T>(this System.Data.Entity.DbSet<T> entitySet, Expression<Func<T, bool>> where, QueryOptimizer optimizer)");
            sb.AppendLine("			where T : System.Data.Entity.DbSet<T>, " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			entitySet.Where(where).Delete(optimizer: optimizer, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Delete<T>(this System.Data.Entity.DbSet<T> entitySet, Expression<Func<T, bool>> where, string connectionString)");
            sb.AppendLine("			where T : System.Data.Entity.DbSet<T>, " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			entitySet.Where(where).Delete(optimizer: null, connectionString: connectionString);");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region Update Extensions
            sb.AppendLine("		#region Update Extensions");
            sb.AppendLine();
            sb.AppendLine("	    private static readonly string[] _updateableAuditFields = new[]");
            sb.AppendLine("	    {");
            sb.AppendLine("	        \"" + _model.Database.ModifiedByDatabaseName + "\",");
            sb.AppendLine("	        \"" + _model.Database.ModifiedDateDatabaseName + "\"");
            sb.AppendLine("	    };");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this IQueryable<T> query, Expression<Func<T, T>> obj)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			query.Update(obj: obj, optimizer: null, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this IQueryable<T> query, Expression<Func<T, T>> obj, QueryOptimizer optimizer)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			query.Update(obj: obj, optimizer: optimizer, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this IQueryable<T> query, Expression<Func<T, T>> obj, string connectionString)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			query.Update(obj: obj, optimizer: null, connectionString: connectionString);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this IQueryable<T> query, Expression<Func<T, T>> obj, QueryOptimizer optimizer, string connectionString)");
            sb.AppendLine("			where T : " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine();
            sb.AppendLine("			if (optimizer == null)");
            sb.AppendLine("				optimizer = new QueryOptimizer();");
            sb.AppendLine("			if (query == null)");
            sb.AppendLine("				throw new Exception(\"Query must be set\");");
            sb.AppendLine();
            sb.AppendLine("			//There is nothing to do");
            sb.AppendLine("			if (query.ToString().Replace(\"\\r\", string.Empty).Split(new char[] { '\\n' }).LastOrDefault().Trim() == \"WHERE 1 = 0\")");
            sb.AppendLine("				return;");
            sb.AppendLine();
            sb.AppendLine("			var instanceKey = Guid.Empty;");
            sb.AppendLine("			System.Data.Entity.Core.Objects.ObjectContext objectContext = null;");
            sb.AppendLine("			try");
            sb.AppendLine("			{");
            sb.AppendLine();
            sb.AppendLine("				var propContext = query.Provider.GetType().GetProperty(\"InternalContext\");");
            sb.AppendLine("				if (propContext != null)");
            sb.AppendLine("				{");
            sb.AppendLine("					var context = propContext.GetValue(query.Provider);");
            sb.AppendLine("					if (context != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var oc = context.GetType().GetProperty(\"ObjectContext\").GetValue(context) as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						objectContext = oc as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						instanceKey = ((IContext)context.GetType().GetProperty(\"Owner\").GetValue(context)).InstanceKey;");
            sb.AppendLine("						if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("						{");
            sb.AppendLine("							var propCs = context.GetType().GetProperty(\"OriginalConnectionString\");");
            sb.AppendLine("							if (propCs != null) connectionString = (string)propCs.GetValue(context);");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine();
            sb.AppendLine("				if (instanceKey == Guid.Empty)");
            sb.AppendLine("				{");
            sb.AppendLine("					var context2 = query.Provider.GetType().GetField(\"_context\", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);");
            sb.AppendLine("					if (context2 != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var context = context2.GetValue(query.Provider);");
            sb.AppendLine("						objectContext = context as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						var qq = objectContext.InterceptionContext.DbContexts.First() as " + this.GetLocalNamespace() + ".I" + _model.ProjectName + "Entities;");
            sb.AppendLine("						instanceKey = qq.InstanceKey;");
            sb.AppendLine("						if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("						{");
            sb.AppendLine("							connectionString = Util.StripEFCS2Normal(objectContext.Connection.ConnectionString);");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine();
            sb.AppendLine("				if (instanceKey == Guid.Empty)");
            sb.AppendLine("					throw new Exception(\"Unknown context\");");
            sb.AppendLine();
            sb.AppendLine("				if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("				{");
            sb.AppendLine("					var propContext2 = query.GetType().GetProperty(\"Context\");");
            sb.AppendLine("					if (propContext2 != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var context = propContext2.GetValue(query) as System.Data.Entity.Core.Objects.ObjectContext;");
            sb.AppendLine("						if (context != null)");
            sb.AppendLine("						{");
            sb.AppendLine("							var builder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(context.Connection.ConnectionString);");
            sb.AppendLine("							if (!string.IsNullOrWhiteSpace(builder.ProviderConnectionString))");
            sb.AppendLine("							{");
            sb.AppendLine("								objectContext = context;");
            sb.AppendLine("								connectionString = builder.ProviderConnectionString;");
            sb.AppendLine("							}");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			catch { }");
            sb.AppendLine();

            sb.AppendLine("			System.Data.Entity.Core.Objects.ObjectParameterCollection existingParams = null;");
            sb.AppendLine("			{");
            sb.AppendLine("			    var objectQuery = query as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            sb.AppendLine("			    if (objectQuery == null)");
            sb.AppendLine("			    {");
            sb.AppendLine("			        var internalQueryField = query.GetType().GetProperty(\"InternalQuery\", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(query);");
            sb.AppendLine("			        if (internalQueryField != null)");
            sb.AppendLine("			            objectQuery = internalQueryField.GetType().GetProperty(\"ObjectQuery\").GetValue(internalQueryField) as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            sb.AppendLine("			    }");
            sb.AppendLine();
            sb.AppendLine("			    if (objectQuery != null)");
            sb.AppendLine("			    {");
            sb.AppendLine("			        var ss2 = objectQuery.ToTraceString(); //DO NOT REMOVE! must call this to init params");
            sb.AppendLine("			        existingParams = objectQuery.GetType().GetProperty(\"Parameters\").GetValue(objectQuery) as System.Data.Entity.Core.Objects.ObjectParameterCollection;");
            sb.AppendLine("			    }");
            sb.AppendLine("			}");
            sb.AppendLine();

            sb.AppendLine("			var startTime = DateTime.Now;");
            sb.AppendLine("			var changedList = new Dictionary<string, object>();");
            sb.AppendLine();
            sb.AppendLine("			#region Parse Tree");
            sb.AppendLine("			var propBody = obj.GetType().GetProperty(\"Body\");");
            sb.AppendLine("			if (propBody != null)");
            sb.AppendLine("			{");
            sb.AppendLine("				var body = propBody.GetValue(obj);");
            sb.AppendLine("				if (body != null)");
            sb.AppendLine("				{");
            sb.AppendLine("					var propBindings = body.GetType().GetProperty(\"Bindings\");");
            sb.AppendLine("					if (propBindings != null)");
            sb.AppendLine("					{");
            sb.AppendLine("						var members = (IEnumerable<System.Linq.Expressions.MemberBinding>)propBindings.GetValue(body);");
            sb.AppendLine("						foreach (System.Linq.Expressions.MemberAssignment item in members)");
            sb.AppendLine("						{");
            sb.AppendLine("							var name = item.Member.Name;");
            sb.AppendLine("							object value = null;");
            sb.AppendLine();
            sb.AppendLine("							if (item.Expression.Type == typeof(int?))");
            sb.AppendLine("								value = CompileValue<int?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(int))");
            sb.AppendLine("								value = CompileValue<int>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(string))");
            sb.AppendLine("								value = CompileValue<string>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(bool?))");
            sb.AppendLine("								value = CompileValue<bool?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(bool))");
            sb.AppendLine("								value = CompileValue<bool>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(byte?))");
            sb.AppendLine("								value = CompileValue<byte?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(byte))");
            sb.AppendLine("								value = CompileValue<byte>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(char?))");
            sb.AppendLine("								value = CompileValue<char?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(char))");
            sb.AppendLine("								value = CompileValue<char>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(decimal?))");
            sb.AppendLine("								value = CompileValue<decimal?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(decimal))");
            sb.AppendLine("								value = CompileValue<decimal>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(double?))");
            sb.AppendLine("								value = CompileValue<double?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(double))");
            sb.AppendLine("								value = CompileValue<double>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(float?))");
            sb.AppendLine("								value = CompileValue<float?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(float))");
            sb.AppendLine("								value = CompileValue<float>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(long?))");
            sb.AppendLine("								value = CompileValue<long?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(long))");
            sb.AppendLine("								value = CompileValue<long>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(short?))");
            sb.AppendLine("								value = CompileValue<short?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(short))");
            sb.AppendLine("								value = CompileValue<short>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(DateTime?))");
            sb.AppendLine("								value = CompileValue<DateTime?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(DateTime))");
            sb.AppendLine("								value = CompileValue<DateTime>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else if (item.Expression.Type == typeof(Guid?))");
            sb.AppendLine("								value = CompileValue<Guid?>(item.Expression);");
            sb.AppendLine("							else if (item.Expression.Type == typeof(Guid))");
            sb.AppendLine("								value = CompileValue<Guid>(item.Expression);");
            sb.AppendLine();
            sb.AppendLine("							else");
            sb.AppendLine("								throw new Exception(\"Data type is not handled '\" + item.Expression.Type.Name + \"'\");");
            sb.AppendLine();
            sb.AppendLine("							changedList.Add(name, value);");
            sb.AppendLine("						}");
            sb.AppendLine("					}");
            sb.AppendLine("					else");
            sb.AppendLine("					{");
            sb.AppendLine("						throw new Exception(\"Update statement must be in format 'm => new Entity { Field = 0 }'\");");
            sb.AppendLine("					}");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			#endregion");
            sb.AppendLine();
            sb.AppendLine("			//Create a mapping for inheritance");
            sb.AppendLine("			var mapping = new List<UpdateSqlMapItem>();");
            sb.AppendLine("			IReadOnlyBusinessObject theObj = new T();");
            sb.AppendLine("			do");
            sb.AppendLine("			{");
            sb.AppendLine("				var md = theObj.GetMetaData();");
            sb.AppendLine("			    var mdFields = md.GetFields();");
            sb.AppendLine("				var audit = theObj as IAuditable;");
            sb.AppendLine("				if (audit != null && audit.IsModifyAuditImplemented)");
            sb.AppendLine("				{");
            sb.AppendLine("					// For auditable entities, we need to include the updateable fields in the field list so that");
            sb.AppendLine("					// the SQl generator can find them.");
            sb.AppendLine("					mdFields.AddRange(_updateableAuditFields);");
            sb.AppendLine("				}");
            sb.AppendLine("				mapping.Add(new UpdateSqlMapItem { TableName = md.GetTableName(), FieldList = mdFields, Schema = md.Schema(), Metadata = md });");
            sb.AppendLine("				var newT = md.InheritsFrom();");
            sb.AppendLine("				if (newT == null)");
            sb.AppendLine("					theObj = default(T);");
            sb.AppendLine("				else");
            sb.AppendLine("					theObj = (IReadOnlyBusinessObject)Activator.CreateInstance(newT, false);");
            sb.AppendLine("			} while (theObj != null);");
            sb.AppendLine();
            sb.AppendLine("			var paramIndex = 0;");
            sb.AppendLine("			var parameters = new List<System.Data.SqlClient.SqlParameter>();");
            sb.AppendLine("			foreach (var key in changedList.Keys)");
            sb.AppendLine("			{");
            sb.AppendLine("				var map = mapping.First(x => x.FieldList.Any(z => z == key));");
            sb.AppendLine("				var fieldSql = map.SqlList;");
            sb.AppendLine("				var value = changedList[key];");
            sb.AppendLine("				if (value == null)");
            sb.AppendLine("					fieldSql.Add(\"[\" + map.Metadata.GetDatabaseFieldName(key) + \"] = NULL\");");
            sb.AppendLine("				else if (value is string)");
            sb.AppendLine("				{");
            sb.AppendLine("					fieldSql.Add(\"[\" + map.Metadata.GetDatabaseFieldName(key) + \"] = @param\" + paramIndex);");
            sb.AppendLine("					parameters.Add(new System.Data.SqlClient.SqlParameter { ParameterName = \"@param\" + paramIndex, DbType = System.Data.DbType.String, Value = changedList[key] });");
            sb.AppendLine("				}");
            sb.AppendLine("				else if (value is DateTime)");
            sb.AppendLine("				{");
            sb.AppendLine("					fieldSql.Add(\"[\" + map.Metadata.GetDatabaseFieldName(key) + \"] = @param\" + paramIndex);");
            sb.AppendLine("					parameters.Add(new System.Data.SqlClient.SqlParameter { ParameterName = \"@param\" + paramIndex, DbType = System.Data.DbType.DateTime, Value = changedList[key] });");
            sb.AppendLine("				}");
            sb.AppendLine("				else");
            sb.AppendLine("				{");
            sb.AppendLine("					fieldSql.Add(\"[\" + map.Metadata.GetDatabaseFieldName(key) + \"] = @param\" + paramIndex);");
            sb.AppendLine("					parameters.Add(new System.Data.SqlClient.SqlParameter { ParameterName = \"@param\" + paramIndex, Value = changedList[key] });");
            sb.AppendLine("				}");
            sb.AppendLine("				paramIndex++;");
            sb.AppendLine("			}");
            sb.AppendLine();
            sb.AppendLine("			var sb = new System.Text.StringBuilder();");
            sb.AppendLine("			#region Per table code");

            index = 0;
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable == TypedTableConstants.None)).OrderBy(x => x.PascalName))
            {
                var tableName = table.DatabaseName;
                if (table.IsTenant)
                    tableName = _model.TenantPrefix + "_" + table.DatabaseName;

                var innerQueryToString = "((IQueryable<" + GetLocalNamespace() + ".Entity." + table.PascalName + ">)query).Select(x => new { " + string.Join(", ", table.PrimaryKeyColumns.Select(x => "x." + x.PascalName).ToList()) + " }).ToString()";
                if (table.Security.IsValid())
                    innerQueryToString = "((System.Data.Entity.Core.Objects.ObjectQuery)(((System.Data.Entity.Core.Objects.ObjectQuery<" + GetLocalNamespace() + ".Entity." + table.PascalName + ">)query).Select(x => new { " + string.Join(", ", table.PrimaryKeyColumns.Select(x => "x." + x.PascalName).ToList()) + " }))).ToTraceString()";

                var pkSql = string.Join(" AND ", table.PrimaryKeyColumns.Select(x => $"[X].[{x.Name}] = [Extent2].[{x.Name}]"));

                sb.AppendLine("			" + (index == 0 ? string.Empty : "else ") + "if (typeof(T) == typeof(" + GetLocalNamespace() + ".Entity." + table.PascalName + "))");
                sb.AppendLine("			{");
                sb.AppendLine("				sb.AppendLine(\"SET ROWCOUNT \" + optimizer.ChunkSize + \";\");");
                sb.AppendLine("				foreach (var item in mapping.Where(x => x.SqlList.Any()).ToList())");
                sb.AppendLine("				{");
                sb.AppendLine("					sb.AppendLine(\"UPDATE [X] SET\");");
                sb.AppendLine("					sb.AppendLine(string.Join(\", \", item.SqlList));");
                sb.AppendLine("					sb.AppendLine(\"FROM [\" + item.Schema + \"].[\" + LinqSQLParser.GetTableAlias(item.TableName) + \"] AS [X] INNER JOIN (\");");
                sb.AppendLine("					sb.AppendLine(" + innerQueryToString + ");");
                sb.AppendLine("					sb.AppendLine(\") AS [Extent2]\");");
                sb.AppendLine($"					sb.AppendLine(\"ON {pkSql}\");");
                sb.AppendLine("					sb.AppendLine(\"select @@ROWCOUNT\");");
                sb.AppendLine("				}");
                sb.AppendLine("			}");
                index++;
            }
            sb.AppendLine("			else throw new Exception(\"Entity type not found\");");
            sb.AppendLine("			#endregion");

            sb.AppendLine();
            sb.AppendLine("			if (string.IsNullOrEmpty(connectionString))");
            sb.AppendLine("				connectionString = " + _model.ProjectName + "Entities.GetConnectionString();");
            sb.AppendLine();

            sb.AppendLine("			var newParams = new List<System.Data.SqlClient.SqlParameter>();");
            sb.AppendLine("			if (existingParams != null)");
            sb.AppendLine("			{");
            sb.AppendLine("				foreach (var ep in existingParams)");
            sb.AppendLine("				{");
            sb.AppendLine("					newParams.Add(new System.Data.SqlClient.SqlParameter { ParameterName = ep.Name, Value = (ep.Value == null ? System.DBNull.Value : ep.Value) });");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			newParams.AddRange(parameters);");
            sb.AppendLine("			QueryPreCache.AddUpdate(instanceKey, sb.ToString(), newParams, optimizer);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		private class UpdateSqlMapItem");
            sb.AppendLine("		{");
            sb.AppendLine("			public string TableName { get; set; }");
            sb.AppendLine("			public List<string> FieldList { get; set; } = new List<string>();");
            sb.AppendLine("			public List<string> SqlList { get; set; } = new List<string>();");
            sb.AppendLine("			public string Schema { get; set; }");
            sb.AppendLine("			public IMetadata Metadata { get; set; }");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		private static T CompileValue<T>(this Expression exp)");
            sb.AppendLine("		{");
            sb.AppendLine("			var accessorExpression = Expression.Lambda<Func<T>>(exp);");
            sb.AppendLine("			var accessor = accessorExpression.Compile();");
            sb.AppendLine("			return accessor();");
            sb.AppendLine();
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this System.Data.Entity.DbSet<T> entitySet, Expression<Func<T, bool>> where, Expression<Func<T, T>> obj)");
            sb.AppendLine("			where T : class, " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			entitySet.Where(where).Update(obj, optimizer: null, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this System.Data.Entity.DbSet<T> entitySet, Expression<Func<T, bool>> where, Expression<Func<T, T>> obj, QueryOptimizer optimizer)");
            sb.AppendLine("			where T : class, " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			entitySet.Where(where).Update(obj, optimizer: optimizer, connectionString: null);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary />");
            sb.AppendLine("		public static void Update<T>(this System.Data.Entity.DbSet<T> entitySet, Expression<Func<T, bool>> where, Expression<Func<T, T>> obj, string connectionString)");
            sb.AppendLine("			where T : class, " + GetLocalNamespace() + ".IBusinessObject, new()");
            sb.AppendLine("		{");
            sb.AppendLine("			entitySet.Where(where).Update(obj, optimizer: null, connectionString: connectionString);");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		#endregion");

            #endregion

            sb.AppendLine("	}");
            sb.AppendLine();

            #region SequentialId

            sb.AppendLine("	#region SequentialIdGenerator");
            sb.AppendLine();
            sb.AppendLine("	/// <summary>");
            sb.AppendLine("	/// Generates Sequential Guid values that can be used for Sql Server UniqueIdentifiers to improve performance.");
            sb.AppendLine("	/// </summary>");
            sb.AppendLine("	internal class SequentialIdGenerator");
            sb.AppendLine("	{");
            sb.AppendLine("		private readonly object _lock;");
            sb.AppendLine("		private Guid _lastGuid;");
            sb.AppendLine("		// 3 - the least significant byte in Guid ByteArray [for SQL Server ORDER BY clause]");
            sb.AppendLine("		// 10 - the most significant byte in Guid ByteArray [for SQL Server ORDERY BY clause]");
            sb.AppendLine("		private static readonly int[] SqlOrderMap = new int[] { 3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10 };");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Creates a new SequentialId class to generate sequential GUID values.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public SequentialIdGenerator() : this(Guid.NewGuid()) { }");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Creates a new SequentialId class to generate sequential GUID values.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"seed\">Starting seed value.</param>");
            sb.AppendLine("		/// <remarks>You can save the last generated value <see cref=\"LastValue\"/> and then ");
            sb.AppendLine("		/// use this as the new seed value to pick up where you left off.</remarks>");
            sb.AppendLine("		public SequentialIdGenerator(Guid seed)");
            sb.AppendLine("		{");
            sb.AppendLine("			_lock = new object();");
            sb.AppendLine("			_lastGuid = seed;");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Last generated guid value.  If no values have been generated, this will be the seed value.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public Guid LastValue");
            sb.AppendLine("		{");
            sb.AppendLine("			get {");
            sb.AppendLine("				lock (_lock)");
            sb.AppendLine("				{");
            sb.AppendLine("					return _lastGuid;");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			set");
            sb.AppendLine("			{");
            sb.AppendLine("				lock (_lock)");
            sb.AppendLine("				{");
            sb.AppendLine("					_lastGuid = value;");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Generate a new sequential id.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <returns>New sequential id value.</returns>");
            sb.AppendLine("		public Guid NewId()");
            sb.AppendLine("		{");
            sb.AppendLine("			Guid newId;");
            sb.AppendLine("			lock (_lock)");
            sb.AppendLine("			{");
            sb.AppendLine("				var guidBytes = _lastGuid.ToByteArray();");
            sb.AppendLine("				ReorderToSqlOrder(ref guidBytes);");
            sb.AppendLine("				newId = new Guid(guidBytes);");
            sb.AppendLine("				_lastGuid = newId;");
            sb.AppendLine("			}");
            sb.AppendLine();
            sb.AppendLine("			return newId;");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		private static void ReorderToSqlOrder(ref byte[] bytes)");
            sb.AppendLine("		{");
            sb.AppendLine("			foreach (var bytesIndex in SqlOrderMap)");
            sb.AppendLine("			{");
            sb.AppendLine("				bytes[bytesIndex]++;");
            sb.AppendLine("				if (bytes[bytesIndex] != 0)");
            sb.AppendLine("				{");
            sb.AppendLine("					break;");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// IComparer.Compare compatible method to order Guid values the same way as MS Sql Server.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"x\">The first guid to compare</param>");
            sb.AppendLine("		/// <param name=\"y\">The second guid to compare</param>");
            sb.AppendLine("		/// <returns><see cref=\"System.Collections.IComparer.Compare\"/></returns>");
            sb.AppendLine("		public static int SqlCompare(Guid x, Guid y)");
            sb.AppendLine("		{");
            sb.AppendLine("			var result = 0;");
            sb.AppendLine("			var index = SqlOrderMap.Length - 1;");
            sb.AppendLine("			var xBytes = x.ToByteArray();");
            sb.AppendLine("			var yBytes = y.ToByteArray();");
            sb.AppendLine();
            sb.AppendLine("			while (result == 0 && index >= 0)");
            sb.AppendLine("			{");
            sb.AppendLine("				result = xBytes[SqlOrderMap[index]].CompareTo(yBytes[SqlOrderMap[index]]);");
            sb.AppendLine("				index--;");
            sb.AppendLine("			}");
            sb.AppendLine("			return result;");
            sb.AppendLine("		}");
            sb.AppendLine("	}");
            sb.AppendLine();
            sb.AppendLine("	#endregion");
            sb.AppendLine();
            #endregion

            sb.AppendLine("	#endregion");
            sb.AppendLine();
        }

        #endregion

    }
}