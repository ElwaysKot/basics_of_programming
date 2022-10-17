// ПІБ
string studentFullName = "Студент 1";
// Ідентифікатор студента/залікової книжки
unsigned int studentID = 1;
// Вік студента
unsigned short int studentAge = 18;
// Поточний рік навчання (курс)
unsigned short int currentStudyYear = 1;
// Чи навчається студент?
bool isStuding = true;
// Адреса прописки студента
string originAddress = "Київ, Україна";
// Освітній ступінь студента
string degree = "Бакалавр";
// Навчальна група
string group = degree + to_string(currentStudyYear);
unsigned int totalMemoryInBytes = sizeof(studentFullName) +
 sizeof(group) +
 sizeof(studentID) +
 sizeof(studentAge) +
 sizeof(currentStudyYear) +
 sizeof(isStuding) +
 sizeof(originAddress) +
 sizeof(degree);
cout << "ПIБ студента:\t\t" << studentFullName << endl;
cout << "Група:\t\t\t" << group << endl;
cout << "Номер залiкової книжки:\t" << studentID << endl;
cout << "Вiк студента:\t\t" << studentAge << endl;
cout << "Рiк навчання:\t\t" << currentStudyYear << endl;
cout << "Чи ще навчається:\t" << ((isStuding) ? "Так" : "Ні") << endl;
cout << "Адреса:\t\t\t" << originAddress << endl;
cout << "Освiтнiй ступiнь:\t" << degree << endl;
cout << "Загальний об'єм використанної пам'ятi (в байтах):\t" <<
totalMemoryInBytes << endl;