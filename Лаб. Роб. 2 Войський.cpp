// Марка ноутбука
string NotebookMark = "ASUS";
// Кількість ядер процесора
unsigned int CoresOfProcessor = 2;
// Має дискретну відеокарту
bool HaveDiscreteVideocard = true;
// Кількість оперативної пам'яті
unsigned int SizeRAM= 12;
// Кількість USB портів
unsigned int PortsUSB = 3;
// Операційна система
string System = "Windows 10";

unsigned int totalMemoryInBytes = sizeof(NotebookMark) +
 sizeof(CoresOfProcessor) +
 sizeof(HaveDiscreteVideocard) +
 sizeof(SizeRAM) +
 sizeof(PortsUSB) +
 sizeof(isStuding) +
 sizeof(originAddress) +
 sizeof(System);
cout << "Марка ноутбука:\t\t" << NotebookMark << endl;
cout << "Кількість ядер процесора:\t\t\t" << CoresOfProcessor << endl;
cout << "Має дискретну відеокарту:\t" << ((HaveDiscreteVideocard) ? "Так": "Ні") << endl;
cout << "Кількість оперативної пам'яті:\t\t" << SizeRAM << endl;
cout << "Кількість USB портів:\t\t" << por << endl;
cout << "Операційна система:\t" << System << endl;
cout << "Загальний об'єм використанної пам'ятi (в байтах):\t" <<
totalMemoryInBytes << endl;
