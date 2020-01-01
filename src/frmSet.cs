using SinaimgPublisher.Extension;
using SinaimgPublisher.Property;
using SinaimgPublisher.Robot.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinaimgPublisher
{
    public partial class frmSet : Form
    {
        Font ResponseRulesController_qqtypefont_isgroup = new Font(frmSet.DefaultFont, FontStyle.Bold);
        Font ResponseRulesController_qqtypefont_isqq = new Font(frmSet.DefaultFont, FontStyle.Regular);
        string CurrentQqNum = "";
        List<ResponseRule> rr = new List<ResponseRule>();
        List<ResponseRule> tmpforBindingList = new List<ResponseRule>();

        public frmSet()
        {
            InitializeComponent();
            Read();
        }

        private void Read()
        {
            isActiveKeywork.Checked = HandlerProperty.SProperty.isActiveKeywork;
            isActiveResponse.Checked = HandlerProperty.SProperty.isActiveResponse;
            isActiveResponseErrorMessage.Checked = HandlerProperty.SProperty.isActiveResponseErrorMessage;
            isActiveResponseIncorrectMessage.Checked = HandlerProperty.SProperty.isActiveResponseIncorrectMessage;
            isActiveAutocreate.Checked = HandlerProperty.SProperty.isActiveAutocreate;
            isActiveNewUndefinedListToBlackList.Checked = HandlerProperty.SProperty.isActiveNewUndefinedListToBlackList;
            isActiveUndefinedListNotAccept.Checked = HandlerProperty.SProperty.isActiveUndefinedListNotAccept;

            cbisAtMe.Checked = HandlerProperty.SProperty.isAtMe;
            cbUid.Checked = HandlerProperty.SProperty.isuid;
            cbImg.Checked = HandlerProperty.SProperty.isimg;
            cbImgName.Checked = HandlerProperty.SProperty.isimg_name;
            cbImgUrl.Checked = HandlerProperty.SProperty.isimg_url;
            cbProfileUrl.Checked = HandlerProperty.SProperty.isprofile_url;
            cbscreen_name.Checked = HandlerProperty.SProperty.isscreen_name;
            cbdescription.Checked = HandlerProperty.SProperty.isdescription;
            cbgender.Checked = HandlerProperty.SProperty.isgender;
            cbfollow.Checked = HandlerProperty.SProperty.isfollower;
            cbfollower.Checked = HandlerProperty.SProperty.isfollower;
            cburank.Checked = HandlerProperty.SProperty.isurank;
            cbmbtype.Checked = HandlerProperty.SProperty.ismbtype;
            cbmbrank.Checked = HandlerProperty.SProperty.ismbrank;
            cbverified.Checked = HandlerProperty.SProperty.isverified;
            cbverified_type.Checked = HandlerProperty.SProperty.isverified_type;
            tbKeyword.Text = HandlerProperty.SProperty.Keyword;
            tbResponse.Text = HandlerProperty.SProperty.Response;
            tbResponseErrorMessage.Text = HandlerProperty.SProperty.ResponseErrorMessage;
            tbResponseIncorrectMessage.Text = HandlerProperty.SProperty.ResponseIncorrectMessage;

            rbRuleTypeAll.Checked = HandlerProperty.SProperty.CustomizeRules_RuleType == RuleType.All;
            rbRuleTypeBlackList.Checked = HandlerProperty.SProperty.CustomizeRules_RuleType == RuleType.Blacklist;
            rbRuleTypeWhiteList.Checked = HandlerProperty.SProperty.CustomizeRules_RuleType == RuleType.Whitelist;
            rbRuleTypeUndefined.Checked = HandlerProperty.SProperty.CustomizeRules_RuleType == RuleType.Undefined;
            rbQTypeAll.Checked = HandlerProperty.SProperty.CustomizeRules_QType == QType.All;
            rbQTypeQnum.Checked = HandlerProperty.SProperty.CustomizeRules_QType == QType.Qq;
            rbQTypeGroup.Checked = HandlerProperty.SProperty.CustomizeRules_QType == QType.Group;

            tbCurrentQObject.Text = HandlerProperty.SProperty.CurrentQObject;
            rbObjectQTypeQnum.Checked = HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType == QType.Qq;
            rbObjectQTypeGroup.Checked = HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType == QType.Group;
            rbObjectRuleTypeUndefined.Checked = HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType == RuleType.Undefined;
            rbObjectRuleTypeBlackList.Checked = HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType == RuleType.Blacklist;
            rbObjectRuleTypeWhiteList.Checked = HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType == RuleType.Whitelist;
        }

        private void UpdateViews(QType qtype, RuleType ruletype,string qq ="")
        {
            flpCustomizeRules.Controls.Clear();
            rr = ResponseRulesController.Get();
            tmpforBindingList = new List<ResponseRule>();

            if (rr != null)
            {
                if (rr.Count > 0)
                {
                    rr.ForEach(f =>
                    {
                        if (qtype != QType.All)
                        {
                            if (qtype != f.qtype) { return; }
                        }
                        if (ruletype != RuleType.All)
                        {
                            if (ruletype != f.rtype) { return; }
                        }
                        if (qq != "")
                        {
                            if (!f.qq.Contains(qq)) { return; }
                        }
                        Button QRule = new Button();
                        QRule.FlatStyle = FlatStyle.Flat;
                        QRule.Size = new Size(100, 20);
                        QRule.UseVisualStyleBackColor = false;
                        string sRuleType = Enum.GetName(typeof(RuleType), f.rtype);
                        RuleTypeColor rt = (RuleTypeColor)Enum.Parse(typeof(RuleTypeColor), sRuleType);
                        int tmp = (int)((RuleTypeColor)rt);
                        QRule.BackColor = Color.FromArgb(tmp);
                        QRule.Font = f.qtype == QType.Group ? ResponseRulesController_qqtypefont_isgroup : ResponseRulesController_qqtypefont_isqq;
                        QRule.Tag = f;
                        QRule.Text = f.qq;
                        QRule.MouseClick += QRule_MouseClick;
                        flpCustomizeRules.Controls.Add(QRule);

                        tmpforBindingList.Add(f);
                    });
                }
            }

            var bindingList = new BindingList<ResponseRule>(tmpforBindingList);
            var bindingSource = new BindingSource(bindingList, null);
            dgvRules.DataSource = bindingSource;
            dgvRules.Columns["rtype"].Visible = false;
            dgvRules.Columns["qtype"].Visible = false;
            dgvRules.Columns["rtypeString"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvRules.Columns["qtypeString"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvRules.Columns["qq"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvRules.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            UpdateDGVSelecter();
            GC.Collect();
        }

        private void UpdateDGVSelecter()
        {
            try
            {
                dgvRules.ClearSelection();
                string _qtype;
                string _qq;
                QType _eqtype;
                foreach (DataGridViewRow r in dgvRules.Rows)
                {
                    _qtype = r.Cells["qtype"].Value.ToString();
                    _qq = r.Cells["qq"].Value.ToString();
                    _eqtype = (QType)Enum.Parse(typeof(QType), _qtype);

                    if (_qq == HandlerProperty.SProperty.CurrentQObject)
                    {
                        if (_eqtype == HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType)
                        {
                            r.Selected = true;
                            dgvRules.FirstDisplayedScrollingRowIndex = dgvRules.SelectedRows[0].Index;
                            break;
                        }
                    }
                }
            }
            catch { }
        }
        private void UpdateSelectorRefDGV()
        {
            if (dgvRules.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvr = dgvRules.SelectedRows[0];
                string _qtype;
                string _qq;

                if (dgvr.Cells["qtype"].Value != null && dgvr.Cells["qq"].Value != null)
                {
                    _qtype = dgvr.Cells["qtype"].Value.ToString();
                    _qq = dgvr.Cells["qq"].Value.ToString();
                    ResponseRule rr = ResponseRulesController.Select(_qq, (QType)Enum.Parse(typeof(QType), _qtype));
                    UpdateSelector(rr);
                }
            }
        }
        private void UpdateSelector(ResponseRule rr)
        {

            tbCurrentQObject.Text = rr.qq;
            HandlerProperty.SProperty.CurrentQObject = tbCurrentQObject.Text;

            if (rr.qtype == QType.Qq)
            {
                rbObjectQTypeQnum.Checked = true;
            }
            else if ((rr.qtype == QType.Group))
            {
                rbObjectQTypeGroup.Checked = true;
            }
            else if ((rr.qtype == QType.Undefined))
            {
                HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType = QType.Undefined;
            }

            switch (rr.rtype)
            {
                case RuleType.Blacklist:
                    rbObjectRuleTypeBlackList.Checked = true;
                    break;
                case RuleType.Whitelist:
                    rbObjectRuleTypeWhiteList.Checked = true;
                    break;
                case RuleType.Undefined:
                    rbObjectRuleTypeUndefined.Checked = true;
                    break;
            }
        }

        private void isActiveKeywork_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveKeywork = isActiveKeywork.Checked;
        }

        private void isActiveResponse_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveResponse = isActiveResponse.Checked;
        }

        private void isActiveResponseErrorMessage_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveResponseErrorMessage = isActiveResponseErrorMessage.Checked;
        }
        private void tbResponseIncorrectMessage_TextChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.ResponseIncorrectMessage = tbResponseIncorrectMessage.Text;
        }

        private void cbisAtMe_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isAtMe = cbisAtMe.Checked;
        }

        private void cbUid_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isuid = cbUid.Checked;
        }

        private void cbImg_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isimg = cbImg.Checked;
        }

        private void cbImgName_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isimg_name = cbImgName.Checked;
        }

        private void cbImgUrl_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isimg_url = cbImgUrl.Checked;
        }

        private void cbProfileUrl_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isprofile_url = cbProfileUrl.Checked;
        }

        private void cbscreen_name_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isscreen_name = cbscreen_name.Checked;
        }

        private void cbdescription_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isdescription = cbdescription.Checked;
        }

        private void cbgender_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isgender = cbgender.Checked;
        }

        private void cbfollow_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isfollow = cbfollow.Checked;
        }

        private void cbfollower_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isfollower = cbfollower.Checked;
        }

        private void cburank_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isurank = cburank.Checked;
        }

        private void cbmbtype_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.ismbtype = cbmbtype.Checked;
        }

        private void cbmbrank_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.ismbrank = cbmbrank.Checked;
        }

        private void tbKeyword_TextChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.Keyword = tbKeyword.Text;
        }

        private void tbResponse_TextChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.Response = tbResponse.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SinaimgPublisherConfig.SaveProperty();
        }

        private void cbverified_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isverified = cbverified.Checked;
        }

        private void cbverified_type_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isverified_type = cbverified_type.Checked;
        }

        private void tbResponseErrorMessage_TextChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.ResponseErrorMessage = tbResponseErrorMessage.Text;
        }
        private void isActiveResponseIncorrectMessage_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveResponseIncorrectMessage = isActiveResponseIncorrectMessage.Checked;
        }
        private void isActiveCustomizeRules_CheckedChanged(object sender, EventArgs e)
        {
            isActiveCustomizeRulesOtherwise.Enabled = isActiveCustomizeRules.Checked;
            HandlerProperty.SProperty.isActiveCustomizeRules = isActiveCustomizeRules.Checked;
        }
        private void isActiveCustomizeRulesOtherwise_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveCustomizeRulesOtherwise = isActiveCustomizeRulesOtherwise.Checked;
        }
        private void tbCurrentQObject_TextChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CurrentQObject = tbCurrentQObject.Text;
        }

        private void rbRuleTypeAll_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_RuleType = RuleType.All;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void rbRuleTypeBlackList_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_RuleType = RuleType.Blacklist;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void rbRuleTypeWhiteList_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_RuleType = RuleType.Whitelist;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void rbRuleTypeUndefined_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_RuleType = RuleType.Undefined;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void rbQTypeAll_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_QType = QType.All;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void rbQTypeQnum_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_QType = QType.Qq;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void rbQTypeGroup_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRules_QType = QType.Group;
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }
        private void rbObjectQTypeQnum_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType = QType.Qq;
        }

        private void rbObjectQTypeGroup_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType = QType.Group;
        }

        private void rbObjectRuleTypeBlackList_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType = RuleType.Blacklist;
        }

        private void rbObjectRuleTypeWhiteList_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType = RuleType.Whitelist;
        }

        private void rbObjectRuleTypeUndefined_Click(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType = RuleType.Undefined;
        }
        private void rbObjectQTypeQnum_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType = QType.Qq;
        }

        private void rbObjectQTypeGroup_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType = QType.Group;
        }
        private void isActiveAutocreate_CheckedChanged(object sender, EventArgs e)
        {
            isActiveNewUndefinedListToBlackList.Enabled = isActiveAutocreate.Checked;
            HandlerProperty.SProperty.isActiveAutocreate = isActiveAutocreate.Checked;
        }

        private void isActiveNewUndefinedListToBlackList_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveNewUndefinedListToBlackList = isActiveNewUndefinedListToBlackList.Checked;
        }

        private void isActiveUndefinedListNotAccept_CheckedChanged(object sender, EventArgs e)
        {
            HandlerProperty.SProperty.isActiveUndefinedListNotAccept = isActiveUndefinedListNotAccept.Checked;
        }
        private void btnObjectdefault_Click(object sender, EventArgs e)
        {
            //cbImg.Checked = false;
            cbUid.Checked = false;
            cbImgName.Checked = true;
            cbImgUrl.Checked = false;

            cbProfileUrl.Checked = true;
            cbscreen_name.Checked = true;
            cbdescription.Checked = true;
            cbgender.Checked = true;

            cbfollow.Checked = false;
            cbfollower.Checked = false;
            cburank.Checked = false;
            cbmbtype.Checked = false;
            cbmbrank.Checked = false;
            cbverified.Checked = false;
            cbverified_type.Checked = false;
        }

        private void btnObjectAll_Click(object sender, EventArgs e)
        {
            //cbImg.Checked = true;
            cbUid.Checked = true;
            cbImgName.Checked = true;
            cbImgUrl.Checked = true;
            cbProfileUrl.Checked = true;
            cbscreen_name.Checked = true;
            cbdescription.Checked = true;
            cbgender.Checked = true;
            cbfollow.Checked = true;
            cbfollower.Checked = true;
            cburank.Checked = true;
            cbmbtype.Checked = true;
            cbmbrank.Checked = true;
            cbverified.Checked = true;
            cbverified_type.Checked = true;
        }

        private void frmSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Read();
        }
        private void QRule_MouseClick(object sender, MouseEventArgs e)
        {
            ResponseRule rr = ((ResponseRule)((Button)sender).Tag);
            UpdateSelector(rr);
        }
        private void btnCustomizeRulesAdd_Click(object sender, EventArgs e)
        {
            ResponseRulesController.AddorUpdate(new ResponseRule
            {
                qq = HandlerProperty.SProperty.CurrentQObject,
                qtype = HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType,
                rtype = HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType,
            });
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
            UpdateDGVSelecter();
        }

        private void btnCustomizeRulesDel_Click(object sender, EventArgs e)
        {
            ResponseRulesController.Remove(new ResponseRule
            {
                qq = HandlerProperty.SProperty.CurrentQObject,
                qtype = HandlerProperty.SProperty.CustomizeRulesCurrentObject_QType,
                rtype = HandlerProperty.SProperty.CustomizeRulesCurrentObject_RuleType,
            });
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void btnCustomizeRulesDelA_Click(object sender, EventArgs e)
        {
            ResponseRulesController.RemoveAll();
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void btnCustomizeRulesAddB_Click(object sender, EventArgs e)
        {

         //   WINAPI.MessageBox(new IntPtr(0), String.Join(",  ", _API.GetGroupList().GroupInfos.Select(s=>s.GroupName).ToList().ToArray()), "Result", 40000);

            _API.GetGroupList_Async((data) =>
            {
                if (data != null)
                {
                    if (data.GroupInfos.Count > 0)
                    {
                        //   WINAPI.MessageBox(new IntPtr(0), data.GroupInfos.Count.ToString(), "GroupInfosCount",40000);
                        //   WINAPI.MessageBox(new IntPtr(0), String.Join(",  ", data.GroupInfos.Select(s => s.GroupName).ToList().ToArray()), "Result", 40000);
                        data.GroupInfos.ForEach(g =>
                        {
                            ResponseRulesController.AddorUpdate(new ResponseRule
                            {
                                qq = g.GroupNumber.ToString(),
                                Name = g.GroupName,
                                qtype = QType.Group,
                                rtype = RuleType.Undefined
                            });
                        });

                        UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
                        UpdateDGVSelecter();
                    }
                }
            });
        }

        private void tbSearchByQqNum_TextChanged(object sender, EventArgs e)
        {

            if (cbSearchByQqNum.Checked)
            {
                CurrentQqNum = tbSearchByQqNum.Text;
            }
            else
            {
                CurrentQqNum = "";
            }
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void cbSearchByQqNum_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchByQqNum.Checked)
            {
                CurrentQqNum = tbSearchByQqNum.Text;
            }
            else
            {
                CurrentQqNum = "";
            }
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        }

        private void tcRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDGVSelecter();
        }

        private void dgvRules_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateSelectorRefDGV();
        }

        private void frmSet_Shown(object sender, EventArgs e)
        {
       //     UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
        //    UpdateDGVSelecter();
        }

        private void frmSet_VisibleChanged(object sender, EventArgs e)
        {
            UpdateViews(HandlerProperty.SProperty.CustomizeRules_QType, HandlerProperty.SProperty.CustomizeRules_RuleType, CurrentQqNum);
            UpdateDGVSelecter();
        }
    }
}
