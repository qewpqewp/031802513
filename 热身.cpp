#include<iostream>
#include <fstream>
using namespace std;
int main(){
	char line[256];  
	ifstream in("Request.txt");
	ofstream out("output.txt");
	while (!in.eof() )  
	{  
   in.getline (line,250);  
       out << line<<endl ; 
	}
return 0;

}
