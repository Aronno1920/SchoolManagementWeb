using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoreLibrary
{
    public static class PopulateLists
    {
        #region Location Data Load
        /// <summary>
        /// Return all active divisions (DivisionId, Division)
        /// </summary>
        public static DataTable GetDivisionList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Division");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetDivisionList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active districts (DistrictId, District)
        /// </summary>
        public static DataTable GetDistrictList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "District");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetDistrictList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active districts of a division (DistrictId, District)
        /// </summary>
        public static DataTable GetDistrictListByDivision(String sDivisionID)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("SearchBy", sDivisionID);
                _param.Add("Action", "DistrictByDivision");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetDistrictListByDivision", ex);
            }
            return _returnTable;
        }



        /// <summary>
        /// Return all Session Fee Related Sub-Head (SessionId, SessionHead)
        /// </summary>
        public static DataTable GetSessionFeeHead()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "GetSessionHead");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_SESSION_DETAILS", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSessionFeeHead", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active upazillas of a district (UpazillaId, Upazilla)
        /// </summary>
        public static DataTable GetUpazillaListByDistrict(String sDistrictID)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("SearchBy", sDistrictID);
                _param.Add("Action", "UpazillaByDistrict");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetUpazillaListByDistrict", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active post offices of a upazilla (PostOfficeId, PostOffice, PostCode)
        /// </summary>
        public static DataTable GetPostOfficeListByUpazilla(String sUpazillaID)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("SearchBy", sUpazillaID);
                _param.Add("Action", "PostOfficeByUpazilla");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetPostOfficeListByUpazilla", ex);
            }
            return _returnTable;
        }

        #endregion

        #region Search all active data

        /// <summary>
        /// Return all active admission year (SessionYear, SessionTitle)
        /// </summary>
        public static DataTable GetSessionList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_ACADEMIC_YEAR", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSessionList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active admission year (SessionYear, SessionTitle)
        /// </summary>
        public static DataTable GetSessionActiveList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadActive");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_ACADEMIC_YEAR", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSessionList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active holiday types (ReasonId, Reason)
        /// </summary>
        public static DataTable GetLeaveReasonList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "LeaveReason");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetLeaveReasonList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active scholarship types (ScholarshipId, ScholarshipName)
        /// </summary>
        public static DataTable GetWaiverType(String sStudentId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("StudentId", sStudentId);
                _param.Add("Action", "WaiverType");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_WAIVER", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetWaiverType", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all Unpaid and Un-waiverd scholarship types (ScholarshipId, ScholarshipName)
        /// </summary>
        public static DataTable GetWaiverTypeForApprove(String sStudentId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("StudentId", sStudentId);
                _param.Add("Action", "WaiverTypeForApprove");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_WAIVER", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetWaiverType", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return current admission year & admission fees only. Return type datatable value.
        /// </summary>
        public static DataTable GetCurrentAdmissionYearFees()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "CurrentYear");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_ACADEMIC_YEAR", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetCurrentAdmissionYearFees", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active class information (ClassId,ClassName,ClassNameBangla,ClassNameAlias)
        /// </summary>
        public static DataTable GetClassList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_CLASS", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetClassList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return only first class information for candidate (ClassId,ClassName,ClassNameBangla,ClassNameAlias)
        /// </summary>
        public static DataTable GetClassForCandidate()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ClassForCandidate");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_CLASS", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetClassList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all class wise active subject information (SubjectId,ClassName,SubjectCode,SubjectName,SubjectNameBangla)
        /// </summary>
        public static DataTable GetSubjectListByClass()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_SUBJECT", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSubjectListByClass", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all class wise active section information (SectionId,ClassName,SectionName,SectionNameBangla,SectionNameAlias)
        /// </summary>
        public static DataTable GetSectionListByClass()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_SECTION", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSectionListByClass", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active day information (DayId,DayName,DayNameBangla,DayNameShort)
        /// </summary>
        public static DataTable GetDayList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Days");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetDayList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active month information (MonthId,MonthName,MonthNameBangla,MonthNameShort)
        /// </summary>
        public static DataTable GetMonthList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Months");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetMonthList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active religion information (ReligionId,ReligionName)
        /// </summary>
        public static DataTable GetReligionList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Religion");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetReligionList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active nationality (NationalityId,NationalityName).
        /// </summary>
        public static DataTable GetNationalityList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Nationality");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetNationalityList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active relationship type (RelationId,RelationName)
        /// </summary>
        public static DataTable GetRelationshipList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Relationship");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetRelationshipList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active fees list (FeesId,ClassId,MonthNo,ClassName,FeesDetails,Amount)
        /// </summary>
        public static DataTable GetFeesScheduleInfo()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Read");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_FEES_SCHEDULE", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetFeesScheduleInfo", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active class duration (DurationId,DurationName,DurationNameBangla,StartTime,EndTime)
        /// </summary>
        public static DataTable GetDurationList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Read");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_DURATION", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetDurationList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active teachers list (TeacherId,TeacherName,TeacherNameBangla)
        /// </summary>
        public static DataTable GetTeacherList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "TeacherList");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_EMPLOYEE_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetTeacherList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active primary school list (SchoolId,SchoolName)
        /// </summary>
        public static DataTable GetSchoolList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "SchoolList");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_OTHER_SCHOOL", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSchoolList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all User Group (GroupId,GroupName)
        /// </summary>
        public static DataTable GetUserGroupAll()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "UserGroup");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_EMPLOYEE_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetUserGroupAll", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active User  (EmployeeId, EmployeeName, EmployeeType)
        /// </summary>
        public static DataTable GetUserList()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "UserList");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_ATTENDANCE_OFFICER", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetUserList", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active collection type list (AccountID, AccountName)
        /// </summary>
        public static DataTable GetFeesType()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "LoadFeesType");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("LoadFeesType", ex);
            }
            return _returnTable;
        }

        public static DataTable GetCurrentBalace(String sAccountID)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("AccountID", sAccountID);
                _param.Add("Action", "CurrentBalace");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_VOUCHER_ENTRY", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetCurrentBalace", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active collection type for a Candidate (TransactionId,TransactionName,TransactionNameBangla)
        /// </summary>
        public static DataTable GetFeesForCandidate()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("Action", "LoadFeesForCandidate");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetCollectionTypeForCandidate", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all Voucher Type Information (VoucherTypeID,VoucherType,Description)
        /// </summary>
        public static DataTable GetVoucherTypeInfo()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "VoucherType");

                DataReader _reader = new DataReader();
                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetVoucherTypeInfo", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all Voucher Head Information For Voucher Entry (AccountId,AccountName,Description)
        /// </summary>
        public static DataTable GetAccountHeadForVoucher()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "AccountHeadForVoucher");

                DataReader _reader = new DataReader();
                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetAccountHeadForVoucher", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return All Bank Information (AccountId,AccountName,Description)
        /// </summary>
        public static DataTable GetBankInformation()
        {
            DataTable _returnTable = new DataTable();
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "BankInformation");

                DataReader _reader = new DataReader();
                _returnTable = _reader.GetDataTableByStoredProcedure("SP_BASIC_INFO", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetBankInformation", ex);
            }
            return _returnTable;
        }

        #endregion

        #region Search all active data by another Id
        /// <summary>
        /// Return all active teachers list (DurationId,DurationName,DurationNameBangla,StartTime,EndTime)
        /// </summary>
        public static DataTable GetSectionListByClass(String sClassId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", sClassId);
                _param.Add("Action", "ReadByClassID");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_SECTION", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSectionListByClass", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return all active teachers list (DurationId,DurationName,DurationNameBangla,StartTime,EndTime)
        /// </summary>
        public static DataTable GetSubjectListByClass(String sClassId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", sClassId);
                _param.Add("Action", "ReadByClassID");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_SUBJECT", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetSubjectListByClass", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return Class And Section Wise Class Routine Information (DurationId,DurationName,DurationNameBangla,StartTime,EndTime)
        /// </summary>
        public static DataTable GetRoutineInfoByClassSection(String sClassId, String sSectionId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("ClassId", sClassId);
                _param.Add("SectionId", sSectionId);
                _param.Add("Action", "ClassRoutine");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_ROUTINE", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetRoutineInfoByClassSection", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return Class And Section Wise Class Routine Information (Bangla) (DurationId,DurationName,DurationNameBangla,StartTime,EndTime)
        /// </summary>
        public static DataTable GetRoutineBanglaByClassSection(String sClassId, String sSectionId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("ClassId", sClassId);
                _param.Add("SectionId", sSectionId);
                _param.Add("Action", "ClassRoutineBangla");

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_ROUTINE", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetRoutineBanglaByClassSection", ex);
            }
            return _returnTable;
        }

        /// <summary>
        /// Return Teacher Wise Class Routine Information (DurationId,DurationName,DurationNameBangla,StartTime,EndTime)
        /// </summary>
        public static DataTable GetRoutineByTeacher(String sTeacherId)
        {
            DataTable _returnTable = new DataTable();
            try
            {
                DataReader _reader = new DataReader();
                Hashtable _param = new Hashtable();
                _param.Add("TeacherId", sTeacherId);

                _returnTable = _reader.GetDataTableByStoredProcedure("SP_TEACHER_WISE_ROUTINE", _param);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError("GetRoutineByTeacher", ex);
            }
            return _returnTable;
        }
        #endregion
    }
}