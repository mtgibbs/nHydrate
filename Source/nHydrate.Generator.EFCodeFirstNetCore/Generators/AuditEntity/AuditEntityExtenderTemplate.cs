#region Copyright (c) 2006-2011 Widgetsphere LLC, All Rights Reserved
//--------------------------------------------------------------------- *
//                          Widgetsphere  LLC                           *
//             Copyright (c) 2006-2011 All Rights reserved              *
//                                                                      *
//                                                                      *
//This file and its contents are protected by United States and         *
//International copyright laws.  Unauthorized reproduction and/or       *
//distribution of all or any portion of the code contained herein       *
//is strictly prohibited and will result in severe civil and criminal   *
//penalties.  Any violations of this copyright will be prosecuted       *
//to the fullest extent possible under law.                             *
//                                                                      *
//THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
//TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
//TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
//CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
//THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF WIDGETSPHERE LLC   *
//                                                                      *
//UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
//PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
//SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY WIDGETSPHERE PRODUCT.    *
//                                                                      *
//THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
//CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF WIDGETSPHERE,      *
//INC.  THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO       *
//INSURE ITS CONFIDENTIALITY.                                           *
//                                                                      *
//THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
//PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
//EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
//THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
//SOURCE CODE CONTAINED HEREIN.                                         *
//                                                                      *
//THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
//--------------------------------------------------------------------- *
#endregion
using System;
using System.Linq;
using nHydrate.Generator.Common.GeneratorFramework;
using System.Text;
using nHydrate.Generator.Common.Util;
using System.Collections.Generic;
using nHydrate.Generator.EFCodeFirstNetCore;
using nHydrate.Generator.Models;
using nHydrate.Generator.Common;

namespace nHydrate.Generator.EFCodeFirstNetCoreNetCore.Generators.EFCSDL
{
    public class AuditEntityExtenderTemplate : EFCodeFirstBaseTemplate
    {
        private StringBuilder sb = new StringBuilder();
        private Table _currentTable = null;

        public AuditEntityExtenderTemplate(ModelRoot model, Table table)
            : base(model)
        {
            _model = model;
            _currentTable = table;
        }

        #region BaseClassTemplate overrides

        public override string FileName
        {
            get { return string.Format("{0}Audit.cs", _currentTable.PascalName); }
        }


        public override string FileContent
        {
            get
            {
                try
                {
                    GenerateContent();
                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        #endregion

        #region GenerateContent

        public void GenerateContent()
        {
            try
            {
                nHydrate.Generator.GenerationHelper.AppendCopyrightInCode(sb, _model);
                sb.AppendLine("namespace " + this.GetLocalNamespace() + ".Audit");
                sb.AppendLine("{");
                sb.AppendLine("	partial class " + _currentTable.PascalName + "Audit");
                sb.AppendLine("	{");
                sb.AppendLine("	}");
                sb.AppendLine("}");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion

    }
}