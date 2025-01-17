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
using System.Text;
using nHydrate.Generator.Models;

namespace nHydrate.Generator.EFDAL.Interfaces.Generators.Entity
{
	public class EntityExtenderTemplate : EFDALInterfaceBaseTemplate
	{
		private readonly StringBuilder sb = new StringBuilder();
		private readonly Table _currentTable = null;

		public EntityExtenderTemplate(ModelRoot model, Table table)
			: base(model)
		{
			_currentTable = table;
		}

		#region BaseClassTemplate overrides

		public override string FileName
		{
			get { return string.Format("I{0}.cs", _currentTable.PascalName); }
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
				sb.AppendLine("namespace " + GetLocalNamespace() + ".Entity");
				sb.AppendLine("{");
				sb.AppendLine("	partial interface I" + _currentTable.PascalName);
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
