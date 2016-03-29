using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace CHAIPROJTEST.Layouts.CHAIPROJTEST
{
    public partial class Experiment : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SPWeb web = SPContext.Current.Web;
                SPList lstProjects = web.Lists.TryGetList("CAProject");


                DataTable dt = lstProjects.Items.GetDataTable();
                gridPRojects.DataSource = dt;
                gridPRojects.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }



        private string _ProjectName, _ProjectManager, _ProjDivision, _ProjPhase, _ProjectStartDate, _ProjectEndDate;


        public string PROJNAME
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }
        public string PROJMANAGERNAME
        {
            get { return _ProjectManager; }
            set { _ProjectManager = value; }
        }
        public string PROJDIVISIONAME
        {
            get { return _ProjDivision; }
            set { _ProjDivision = value; }
        }

        public string PROJPHASE
        {
            get { return _ProjPhase; }
            set { _ProjPhase = value; }
        }

        public string PROJSTARTDATE
        {
            get { return _ProjectStartDate; }
            set { _ProjectStartDate = value; }
        }

        public string PROJENDDATE
        {
            get { return _ProjectEndDate; }
            set { _ProjectEndDate = value; }
        }

        public string CAML_PROJNAME
        {
            get
            {
                return string.Format(@"<Contains><FieldRef Name='Title'/>" +
                                             "<Value Type='Text'>{0}</Value></Contains>", this.PROJNAME);
            }
        }

        public string CAML_PROJMANAGER
        {
            get
            {
                return string.Format(@"<Contains><FieldRef Name='ProjectManager'/>
                                             <Value Type='Text'>{0}</Value></Contains>", this.PROJMANAGERNAME);
            }
        }
        public string CAML_PROJDIVISION
        {
            get
            {
                return string.Format(@"<Contains><FieldRef Name='ProjectDivision'/>
                                             <Value Type='Text'>{0}</Value></Contains>", this.PROJDIVISIONAME);
            }
        }

        public string CAML_PROJPHASEMETHODOLOGY
        {
            get
            {

                return string.Format(@"<Eq><FieldRef Name='PhaseMethodology'/>
                                             <Value Type='Text'>{0}</Value></Eq>", this.PROJPHASE);
            }
        }


        public string CAML_PROJSTARTDATE
        {
            get
            {

                return string.Format(@"<Eq><FieldRef Name='StartDate'/>
                                             <Value Type ='DateTime' IncludeTimeValue='FALSE'>{0}</Value></Eq>", String.Format("{0:s}", Convert.ToDateTime(this.PROJSTARTDATE)));

            }
        }

        public string CAML_PROJENDDATE
        {
            get
            {

                return string.Format(@"<Eq><FieldRef Name='EndDate'/>
                                             <Value Type ='DateTime' IncludeTimeValue='FALSE'>{0}</Value></Eq>", String.Format("{0:s}", Convert.ToDateTime(this.PROJENDDATE)));

            }
        }




        private void BindData()
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList lstProjects = web.Lists.TryGetList("CAProject");

                web.AllowUnsafeUpdates = true;
                SPQuery query = new SPQuery();


                if (!string.IsNullOrEmpty(Convert.ToString(txtProjectName.Text)))
                    this.PROJNAME = txtProjectName.Text.Trim();

                if (!string.IsNullOrEmpty(Convert.ToString(txtProjectManager.Text)))
                    this.PROJMANAGERNAME = txtProjectManager.Text.Trim();

                if (!string.IsNullOrEmpty(Convert.ToString(txtProjectDivision.Text)))
                    this.PROJDIVISIONAME = txtProjectDivision.Text.ToLower().Trim();


                if (!string.IsNullOrEmpty(Convert.ToString(txtMethodology.Text)))
                    this.PROJPHASE = txtMethodology.Text.ToLower().Trim();


                if (!string.IsNullOrEmpty(Convert.ToString(txtStartDate.Text)))
                    this.PROJSTARTDATE = txtStartDate.Text.ToLower().Trim();



                if (!string.IsNullOrEmpty(Convert.ToString(txtEndDate.Text)))
                    this.PROJENDDATE = txtEndDate.Text.ToLower().Trim();

                //Generate CAML Query Dynamically
                query.Query = Convert.ToString(getCAMLSerachQuery());
                // queryTrainingDetails.QueryThrottleMode = SPQueryThrottleOption.Override;
                query.RowLimit = 1000;

                query.IncludeMandatoryColumns = false;
                query.ViewFields = string.Concat(@"<FieldRef Name='Title' />
                                         <FieldRef Name='ProjectManager' />
                                        <FieldRef Name='ProjectDivision' />                                       
                                        <FieldRef Name='PhaseMethodology' />                                      
                                         <FieldRef Name='StartDate' />
                                          <FieldRef Name='EndDate' />");



                query.ViewFieldsOnly = true;
                SPListItemCollection listitems;

                listitems = lstProjects.GetItems(query);
                DataTable dt = null;

                if (listitems != null && listitems.Count > 0)
                {
                    int fieldCount = listitems.Fields.Count;
                    dt = listitems.GetDataTable();
                    gridPRojects.Visible = true;
                    gridPRojects.DataSource = dt;
                    gridPRojects.DataBind();
                }
                else
                {
                    lblError.Text = "No Record(s) Found.";
                    gridPRojects.Visible = false;

                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        //Build CAML Query using AND Operator
        public string BuildCamlQuery
        {
            get
            {
                List<string> objCaml = new List<string>();
                StringBuilder _caml = new StringBuilder();

                if (!string.IsNullOrEmpty(this.PROJNAME))
                    objCaml.Add(this.CAML_PROJNAME);

                if (!string.IsNullOrEmpty(this.PROJMANAGERNAME))
                    objCaml.Add(this.CAML_PROJMANAGER);

                if (!string.IsNullOrEmpty(this.PROJDIVISIONAME))
                    objCaml.Add(this.CAML_PROJDIVISION);


                if (!string.IsNullOrEmpty(Convert.ToString(this.PROJPHASE)))
                    objCaml.Add(this.CAML_PROJPHASEMETHODOLOGY);

                if (!string.IsNullOrEmpty(Convert.ToString(this.PROJSTARTDATE)))
                    objCaml.Add(this.CAML_PROJSTARTDATE);

                if (!string.IsNullOrEmpty(Convert.ToString(this.PROJENDDATE)))
                    objCaml.Add(this.CAML_PROJENDDATE);


                for (int i = 1; i < objCaml.Count; i++)
                {
                    _caml.Append("<And>");
                }

                //Now create a string out of the CMAL snippets in the list.

                for (int i = 0; i < objCaml.Count; i++)
                {
                    string snippet = objCaml[i];
                    _caml.AppendFormat(snippet);
                    if (i == 1)
                    {
                        _caml.Append("</And>");
                    }

                    else if (i > 1)
                    {
                        _caml.Append("</And>");
                    }

                }

                string value = string.Empty;

                if (_caml.ToString().Trim().Length > 0)
                    value = string.Format(@"<Where>{0}</Where>", _caml.ToString().Trim());
                //Return the final CAML query
                return value;
            }

        }

        //Apend Sort Option to CAML Query
        public StringBuilder getCAMLSerachQuery()
        {
            StringBuilder camlQuery = new StringBuilder();
            if (this.BuildCamlQuery != null && this.BuildCamlQuery != string.Empty)
            {
                camlQuery.Append(this.BuildCamlQuery);
            }
            //  camlQuery.AppendFormat(@"<OrderBy><FieldRef Name='{0}' Ascending='{1}' /></OrderBy>", this.SortField, this.SortDirection);
            return camlQuery;
        }


    }
}



