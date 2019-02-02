#include<iostream>
#include<map>
#include<fstream>
#include<string>
#include <sstream>
#include <vector>
#include  <algorithm>
using namespace std;
int main(){
	int num;//黑客数目 
	vector<string> hacker(100);
	char line[256];
	ifstream in("Request.txt");//读取文件 
	map<string,int> data;
	while(!in.eof()){
		//逐行读取并分割数据 
		in.getline(line,200);
	    istringstream temp(line);
	    string sender,receiver;
	    int length;
		temp>>sender>>receiver>>length; 
		
		//统计并判断 
		if(data.count(sender)){
			data[sender]+=length;
			if(data[sender]>1500 && count(hacker.begin(),hacker.end(),sender)==0){
				cout<<sender<<endl;//输出黑客名 
			    hacker[num]=sender;//储存黑客 
			    num++;
			}
		}
		else
		data[sender]=length;
		
		
	}
    //输出数目 
    cout<<num;
    return 0;
} 
