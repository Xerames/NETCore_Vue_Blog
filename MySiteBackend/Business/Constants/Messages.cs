using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added => "Successfully Added.";
        public static string Deleted => "Successfully Deleted.";
        public static string Updated => "Successfully Updated.";

        public static string LogoutSuccessfull=> "Logout successfull";
        public static string PhotoDeleted => "Photo deleted.";
        public static string ImageDeleted => "Image deleted.";

        public static string CategoryNameIsAlreadyExist => "Category name is already exist";
        public static string NotFound => "Not Found";
        public static string TagNameIsAlreadyExist => "Tag name is already exist";
        public static string RoleNameIsAlreadyExist => "Role name is already exist";

        public static string EmailIsAlreadyExist => "Email is already exist";
        public static string UsernameIsAlreadyExist => "Username is already exist";
        public static string RegisterSuccessfully => "Register successfuly please look at your mail box for account confirmation.";
        public static string TokenOrUserNotFound => "Token or User Not Found";
        public static string RefreshTokenNotFound => "Refresh Token Not Found";
        public static string UserNotFound => "User Not Found";
        public static string UsernameNotFound => "There is no user with that username";
        public static string ConfirmYourAccount => "Please confirm your account";

        public static string AlreadyAccountConfirmed => "Already your account confirmed";
        public static string SuccessfullyAccountConfirmed => "Account confirmed successfully.You can login now";
        public static string AccountDontConfirmed => "Account Dont Confirmed";

        public static string InvalidUsernameorPassword => "Invalid username or password";
        public static string IfEmailTrue => "When you fill in your registered email address, you will be sent instructions on how to reset your password.";
        public static string RoleAssignedSuccessfully => "Role assigned successfully";
        public static string PasswordChangedSuccessfully => "Password changed successfully";
        public static string CurrentPasswordIsFalse => "Current password is false";

        public static string PasswordDontMatchWithConfirmation => "Password doesn't match its confirmation";
        public static string PasswordResetSuccessfully => "Password has been reset successfully";

    }
}
