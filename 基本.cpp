#include<iostream>
#include<map>
#include<fstream>
#include<string>
#include <sstream>
#include <vector>
#include  <algorithm>
using namespace std;
int main(){
	int num;//�ڿ���Ŀ 
	vector<string> hacker(100);
	char line[256];
	ifstream in("Request.txt");//��ȡ�ļ� 
	map<string,int> data;
	while(!in.eof()){
		//���ж�ȡ���ָ����� 
		in.getline(line,200);
	    istringstream temp(line);
	    string sender,receiver;
	    int length;
		temp>>sender>>receiver>>length; 
		
		//ͳ�Ʋ��ж� 
		if(data.count(sender)){
			data[sender]+=length;
			if(data[sender]>1500 && count(hacker.begin(),hacker.end(),sender)==0){
				cout<<sender<<endl;
			    hacker[num]==sender;
			    num++;
			}
		}
		else
		data[sender]=length;
		
		
	}
	//����ڿ� 
	for(int i=0;i<=num;i++)
    cout<<hacker[i];
     
    cout<<num;
    return 0;
} 
