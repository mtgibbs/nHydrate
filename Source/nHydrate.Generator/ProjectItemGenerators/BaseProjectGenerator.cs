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
using System.IO;
using nHydrate.Generator.Common.GeneratorFramework;
using nHydrate.Generator.Common.Util;
using nHydrate.Generator.Models;

namespace nHydrate.Generator.ProjectItemGenerators
{
    public abstract class BaseProjectGenerator : IProjectGenerator
    {
        protected ModelRoot _model;

        protected abstract string ProjectTemplate { get; }
        public abstract string LocalNamespaceExtension { get; }

        public virtual string ProjectName
        {
            get { return this.GetLocalNamespace(); }
        }

        public virtual string DefaultNamespace
        {
            get { return nHydrateGeneratorProject.DomainProjectName(_model); }
        }

        public virtual string GetLocalNamespace()
        {
            if (string.IsNullOrEmpty(this.LocalNamespaceExtension))
                return this.DefaultNamespace;
            else
                return this.DefaultNamespace + "." + this.LocalNamespaceExtension;
        }

        protected virtual void OnAfterGenerate()
        {
            //Implement base functionality if needed
        }

        protected virtual void OnInitialize(IModelObject model)
        {
            //Implement base functionality if needed
        }

        #region IProjectGenerator Members

        public void Initialize(IModelObject model)
        {
            _model = (ModelRoot)model;
            OnInitialize(model);
        }

        //public virtual void LoadProject()
        //{
        //  EnvDTEHelper.Instance.CreateProjectFromTemplate(string.Empty, this.ProjectName, this._model.OutputTarget);
        //}

        public virtual void CreateProject()
        {
            try
            {
                //If there is no project defined then do nothing
                if (string.IsNullOrEmpty(this.ProjectTemplate))
                    return;

                var newProject = EnvDTEHelper.Instance.GetProject(ProjectName);
                if (newProject != null)
                    newProject.Delete();

                var templateFullName = Path.Combine(AddinAppData.Instance.ExtensionDirectory, this.ProjectTemplate);

                //Copy the template and project file to a temp folder and perform replacements
                var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempPath);

                //Copy template
                var fi = new FileInfo(templateFullName);
                var targetFile = Path.Combine(tempPath, fi.Name);
                File.Copy(templateFullName, targetFile, true);

                //Copy project
                var sourceFile = templateFullName.Replace(".vstemplate", ".csproj");
                fi = new FileInfo(sourceFile);
                if (File.Exists(sourceFile))
                {
                    targetFile = Path.Combine(tempPath, fi.Name);
                    File.Copy(sourceFile, targetFile, true);
                    fi = new FileInfo(targetFile);
                    this.GenerateCompanySpecificFile(tempPath, fi.Name);
                }

                //Copy the assembly file over
                sourceFile = Path.Combine(AddinAppData.Instance.ExtensionDirectory, "AssemblyInfo.cs");
                if (File.Exists(sourceFile))
                {
                    var propertyPath = Path.Combine(tempPath, "Properties");
                    Directory.CreateDirectory(propertyPath);
                    var t = Path.Combine(propertyPath, "AssemblyInfo.cs");
                    File.Copy(sourceFile, t, true);
                    fi = new FileInfo(t);
                    this.GenerateCompanySpecificFile(propertyPath, fi.Name);
                }

                newProject = EnvDTEHelper.Instance.CreateProjectFromTemplate(targetFile, this.ProjectName, this._model.OutputTarget);
                Directory.Delete(tempPath, true);
                OnAfterGenerate();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GenerateCompanySpecificFile(string tempPath, string fileName)
        {
            try
            {
                var defaultProjectTemplate = Path.Combine(tempPath, fileName);
                var fileData = File.ReadAllText(defaultProjectTemplate);

                var newFileText = fileData.Replace("Acme", _model.CompanyName);
                newFileText = newFileText.Replace("$generatedproject$", this.DefaultNamespace);
                newFileText = newFileText.Replace("$safeprojectname$", this.GetLocalNamespace());
                newFileText = newFileText.Replace("$registeredorganization$", _model.CompanyName);
                newFileText = newFileText.Replace("$projectname$", this.GetLocalNamespace());
                newFileText = newFileText.Replace("$year$", DateTime.Now.Year.ToString());
                newFileText = newFileText.Replace("$guid1$", Guid.NewGuid().ToString());

                File.WriteAllText(defaultProjectTemplate, newFileText);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

    }
}