using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;

namespace Biblioteka.WebPresentation.Models
{
    #region Services

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class CorporateMembership : MembershipProvider, IMembershipService
    {
        private readonly IRepository<User> mRepository;
        private readonly ICurrent mCurrent;

        public CorporateMembership(IRepository<User> repository, ICurrent current)
        {
            mRepository = repository;
            mCurrent = current;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return false;// mRepository.ChangePassword(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            if (answer == "Biblioteka")
                return "";// mRepository.GetPassword(username);
            return "";
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            return "";// mRepository.GetUserByEmail(email);
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 3; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 3; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 3; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Clear; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (answer.Equals("Biblioteka"))
                return "";// mRepository.ResetPassword(username);
            return "";
        }

        public override bool UnlockUser(string userName)
        {
            return false;//.ActivateUser(userName);
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public int MinPasswordLength
        {
            get { return 3; }
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(password.Trim()))
                return false;

            var user = mRepository.GetAll(c=> c.UserName == username && c.Password == password).SingleOrDefault();
            if (user == null)
                return false;
            mCurrent.CurrentUser = user;
            return true;
        }
    }

    public class CorporateRoleMembership : RoleProvider
    {
        private readonly ICurrent mCurrent;

        public CorporateRoleMembership()
        {
            mCurrent = DependencyResolver.Current.GetService<ICurrent>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {

            if (mCurrent.CurrentUser != null)
            {
                var user = mCurrent.CurrentUser;
                if (user.Position.Split(',').Contains(roleName))
                    return true;
            }
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            if (mCurrent.CurrentUser != null)
                return mCurrent.CurrentUser.Position.Split(',');
            return new string[0];
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return Enum.GetNames(typeof(UserPosition)).Any(c => c == roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return Enum.GetNames(typeof(UserPosition));
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {

        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("username is null. Please fill username first", "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            DependencyResolver.Current.GetService<ICurrent>().CurrentUser = null;
            FormsAuthentication.SignOut();
        }
    }

    #endregion
}
