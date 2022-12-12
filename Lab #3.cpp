#include <iostream>
#include <string>
using namespace std;

extern void menu();
extern void show_menu();



extern wstring** enter_email();
extern wstring** enter_real_name();

extern char** enter_password();
extern char** enter_phone_number();
extern char** enter_real_age();


extern char** enter_new_password();


extern wstring** enter_new_email();


extern wstring** enter_discord_email();
extern char** enter_discord_password();


extern wstring** enter_twitter_email();
extern char** enter_twitter_password();


extern wstring** enter_instagram_email();
extern char** enter_instagram_password();

//extern char** enter_date();

extern wstring** enter_hero_name();
extern char** enter_age_hero();




int startupAuthoriz(int argsC,
 char* argsV[])

{
 int passwordLength = 0;
 for (int argumentIndex = 1;
  argumentIndex < argsC;
  argumentIndex++)
 {
  const char* argK = argsV[argumentIndex];
  const char* argV = argsV[argumentIndex + 1];
  if (strcmp(argK, "-login") == 0)
  {
   cout << "You are logged in as\t" << argV << endl;
  }
  else
  {
   if (strcmp(argK, "-password") == 0)
   {
    passwordLength = strlen(argV);
    break;
   }
  }
 }


 return passwordLength;

}
int main(int argsC,
 char* argsV[],
 char* environmentParameters[]
)
{

 setlocale(LC_ALL, "");

 if (argsC > 1)
 {
  return startupAuthoriz(argsC, argsV);
 }
 else
 {
  menu();
 }

 cin.get();
 return 0;
}

#ifndef INPUT_MAIN_SAMPLE_H
#define INPUT_MAIN_SAMPLE_H

void show_menu()
{

 wcout << L"Actions on the account:" << endl;
 wcout << L"1.) Sign Up" << endl;
 wcout << L"2.) Login" << endl;
 wcout << L"3.) Reset password" << endl;
 wcout << L"4.) Reset email" << endl;
 wcout << L"5.) Connect Discord account" << endl;
 wcout << L"6.) Connect Twitter account" << endl;
 wcout << L"7.) Connect Instagram account" << endl;
 wcout << L"8.) Cancel" << endl;

}
wstring** enter_hero_name()
{
 wstring* hero_name = new wstring();
 wcout << L"Enter hero name(login):" << endl;
 wcin.ignore();
 getline(wcin, *hero_name);

 return &hero_name;
}
wstring** enter_email()
{
 wstring* email = new wstring();
 wcout << L"Enter email:" << endl;
 wcin.ignore();
 getline(wcin, *email);

 return &email;
}
wstring** enter_real_name()
{
 wstring* real_name = new wstring();
 wcout << L"Enter nickname:" << endl;
 wcin.ignore();
 getline(wcin, *real_name);

 return &real_name;
}
wstring** enter_new_email()
{
 wstring* new_email = new wstring();
 wcout << L"Enter new email:" << endl;
 wcin.ignore();
 getline(wcin, *new_email);
 
 return &new_email;
}
wstring** enter_discord_email()
{
 wstring* discord_email = new wstring();
 wcout << L"Enter discord email:" << endl;
 wcin.ignore();
 getline(wcin, *discord_email);

 return &discord_email;
}
wstring** enter_twitter_email()
{
 wstring* twitter_email = new wstring();
 wcout << L"Enter twitter email:" << endl;
 wcin.ignore();
 getline(wcin, *twitter_email);

 return &twitter_email;
}
wstring** enter_instagram_email()
{
 wstring* instagram_email = new wstring();
 wcout << L"Enter instagram email:" << endl;
 wcin.ignore();
 getline(wcin, *instagram_email);

 return &instagram_email;
}
char** enter_password()
{
 const short int MAX_PASSWORD_LENGTH = 16;//16 max
 char* password = new char[MAX_PASSWORD_LENGTH];

 wcout << L"Enter password:" << endl;
 cin >> password;
 return &password;
}
char** enter_phone_number()
{
 const short int MAX_PASSWORD_LENGTH = 16;//38 000 000 00 00
 char* phone_number = new char[MAX_PASSWORD_LENGTH];

 wcout << L"Enter phone number:" << endl;
 cin >> phone_number;
 return &phone_number;
}
char** enter_real_age()
{
 const short int MAX_PASSWORD_LENGTH = 4;//4 max
 char* real_age = new char[MAX_PASSWORD_LENGTH];

 wcout << L"Enter real age:" << endl;
 cin >> real_age;
 return &real_age;
}
char** enter_date()
