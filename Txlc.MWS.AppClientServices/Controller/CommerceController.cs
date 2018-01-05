using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.CommerceModel;
using Trace.AppModel.DataUpModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class CommerceController : BaseModule
    {
        public CommerceController()
            : base("hzsy/api/commerce")
        {
            Post["/commerceScore"] = CommerceScore;
            Post["/commerceStandard"] = CommerceStandard;
            Post["/commerceDataUp"] = CommerceDataUp;
            Post["/commerceAllInfo"] = CommerceAllInfo;
            Post["/commerceStandardInfo"] = CommerceStandardInfo;
            Post["/standardCompList"] = StandardCompList;
            Post["/standardAnimalList"] = StandardAnimalList;
            Post["/standardMarketList"] = StandardMarketList;
            Post["/standardMarketBusList"] = StandardMarketBusList;
            Post["/standardRetailList"] = StandardRetailList;
            Post["/standardRetailBusList"] = StandardRetailBusList;
            Post["/standardSuperMarketList"] = StandardSuperMarketList;
            Post["/standardTeamList"] = StandardTeamList;
            Post["/dataUpAllInfo"] = DataUpAllInfo;
            Post["/dataUpStandardList"] = DataUpStandardList;
            Post["/dataUpStandardAnimal"] = DataUpStandardAnimal;
            Post["/dataUpStandardMarket"] = DataUpStandardMarket;
            Post["/dataUpStandardRetail"] = DataUpStandardRetail;
            Post["/dataUpStandardSuper"] = DataUpStandardSuper;
            Post["/dataUpStandardTeam"] = DataUpStandardTeam;
            Post["/dataUpGrow"] = DataUpGrow;
            Post["/dataUpQualityList"] = DataUpQualityList;
            Post["/dataUpMeatQuality"] = DataUpMeatQuality;
            Post["/dataUpMeatQualityDeatil"] = DataUpMeatQualityDeatil;
            Post["/dataUpVegQuality"] = DataUpVegQuality;
            Post["/dataUpVegQualityDeatil"] = DataUpVegQualityDeatil;
            Post["/report"] = Report;

        }
        /// <summary>
        /// 考核总分数
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CommerceScore(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<CommerceScoreModel> info = messageBll.GetCommerceScore(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<CommerceScoreModel>>(info, "获取考核总得分成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("考核总得分为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取考核总得分异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取考核总得分异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 企业达标情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CommerceStandard(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                CommerceStandardModel info = messageBll.GetCommerceStandard(recdata.data);
                if (info != null)
                {
                    return this.SendData<CommerceStandardModel>(info, "获取考核达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("考核达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取考核达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取考核达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据上报情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CommerceDataUp(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<CommerceDataUpModel> info = messageBll.GetCommerceDataUp(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<CommerceDataUpModel>>(info, "获取上报情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据上报情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "数据上报情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "数据上报情况异常：" + ex.Message);

            }
        }

        /// <summary>
        /// 商务考核总体情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CommerceAllInfo(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<CommerceScoreModel> info = messageBll.GetCommerceAllInfo(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<CommerceScoreModel>>(info, "获取考核总体情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("考核总体情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取总体情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取考核总体情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 达标运行情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CommerceStandardInfo(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                CommerceStandardModel info = messageBll.GetCommerceStandard(recdata.data);
                if (info != null)
                {
                    return this.SendData<CommerceStandardModel>(info, "获取考核达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("考核达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取考核达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取考核达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 肉类、蔬菜运行达标情况列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardCompList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<ComStandCompModel> list = messageBll.GetStandardCompList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<ComStandCompModel>>(list, "获取肉菜运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("肉菜运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取肉菜运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取肉菜运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 屠宰场运行达标情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardAnimalList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardAnimalList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取屠宰场运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("屠宰场运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取屠宰场运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取屠宰场运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 批发市场运行达标情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardMarketList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardMarketList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取批发市场运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("批发市场运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取批发市场运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取批发市场运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 批发市场经营户达标情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardMarketBusList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardMarketBusList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取批发市场经营户运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("批发市场经营户运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取批发市场经营户运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取批发市场经营户运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 菜市场运行达标情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardRetailList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardRetailList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取菜市场运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("菜市场运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取菜市场运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取菜市场运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 菜市场 经营户
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardRetailBusList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardRetailBusList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取菜市场经营户运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("菜市场经营户运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取菜市场经营户运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取菜市场经营户运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 超市运行达标情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardSuperMarketList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardSuperMarketList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取超市运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("超市运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取超市运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取超市运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 团体运行情况
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string StandardTeamList(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                List<StandardCompModel> list = messageBll.GetStandardTeamList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<StandardCompModel>>(list, "获取团体运行达标情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("团体运行达标情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取团体运行达标情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取团体运行达标情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据总体报送得分
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpAllInfo(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                DataUpAllInfoModel list = messageBll.GetDataUpAllInfo(recdata.data);
                if (list != null)
                {
                    return this.SendData<DataUpAllInfoModel>(list, "获取数据总体报送得分情况成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据总体报送得分情况为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据总体报送得分情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据总体报送得分情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据达标情况列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpStandardList(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpStandardListModel> list = messageBll.GetDataUpStandardList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpStandardListModel>>(list, "获取数据-达标情况列表成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-达标情况列表为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据-达标情况列表情况异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据-达标情况列表情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据-达标情况-屠宰场
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpStandardAnimal(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpCompListModel> list = messageBll.GetDataUpAnimal(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpCompListModel>>(list, "数据-达标情况-屠宰场成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-达标情况-屠宰场为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据-达标情况-屠宰场异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据-达标情况-屠宰场情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据-达标情况-批发市场
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpStandardMarket(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpCompListModel> list = messageBll.GetDataUpMarket(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpCompListModel>>(list, "数据-达标情况-批发市场成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-达标情况-批发市场为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据-达标情况-批发市场异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据-达标情况-批发市场情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据-达标情况-菜市场
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpStandardRetail(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpCompListModel> list = messageBll.GetDataUpRetail(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpCompListModel>>(list, "数据-达标情况-菜市场成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-达标情况-菜市场为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据-达标情况-菜市场异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据-达标情况-菜市场情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据-达标情况-超市
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpStandardSuper(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpCompListModel> list = messageBll.GetDataUpSuper(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpCompListModel>>(list, "数据-达标情况-超市成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-达标情况-超市为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据-达标情况-超市异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据-达标情况-超市情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据-达标情况-团体
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpStandardTeam(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpCompListModel> list = messageBll.GetDataUpTeam(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpCompListModel>>(list, "数据-达标情况-团体成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-达标情况-团体为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据-达标情况-团体异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据-达标情况-团体情况异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据增长情况列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpGrow(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpGrowModel> list = messageBll.GetDataUpGrow(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpGrowModel>>(list, "数据增长情况列表成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据增长情况列表为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取数据增长情况列表异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取数据增长情况列表异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 数据-数据质量得分列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpQualityList(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpQualityModel> list = messageBll.GetDataUpQualityList(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpQualityModel>>(list, "数据-数据质量得分列表成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("数据-数据质量得分列表为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "数据-数据质量得分列表异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "数据-数据质量得分列表异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 肉类链条合成
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpMeatQuality(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpComposeModel> list = messageBll.GetDataUpMeatQuality(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpComposeModel>>(list, "获取肉类链条合成成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("肉类链条合成为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "肉类链条合成异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "肉类链条合成异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 肉类链条合成明细
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpMeatQualityDeatil(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpComposeDetailModel> list = messageBll.GetDataUpMeatQualityDetail(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpComposeDetailModel>>(list, "获取肉类链条合成明细成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("肉类链条合成明细为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "肉类链条合成明细异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "肉类链条合成明细异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 蔬菜链条合成
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpVegQuality(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpComposeModel> list = messageBll.GetDataUpVegQuality(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpComposeModel>>(list, "获取菜类链条合成成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("菜类链条合成为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "菜类链条合成异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "菜类链条合成异常：" + ex.Message);

            }
        }

        /// <summary>
        /// 菜链条合成明细
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DataUpVegQualityDeatil(dynamic _)
        {
            var recdata = this.GetResquetData<DataUpInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                DataUpBusiness messageBll = new DataUpBusiness();
                List<DataUpComposeDetailModel> list = messageBll.GetDataUpVegQualityDetail(recdata.data);
                if (list != null)
                {
                    return this.SendData<List<DataUpComposeDetailModel>>(list, "获取蔬菜链条合成明细成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("蔬菜链条合成明细为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "蔬菜链条合成明细异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "蔬菜链条合成明细异常：" + ex.Message);

            }
        }

        public string Report(dynamic _)
        {
            var recdata = this.GetResquetData<CommerceInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommerceBusiness messageBll = new CommerceBusiness();
                CommerceReportModel list = messageBll.GetReport(recdata.data);
                if (list != null)
                {
                    return this.SendData<CommerceReportModel>(list, "获取考核报告成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("考核报告为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取考核报告异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取考核报告异常：" + ex.Message);

            }
        }
    }
}