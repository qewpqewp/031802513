#include<stdio.h>

int main(){
	char num[20]; //输入数字存放 
	int len =0;//数字长度 
	int count[10]={0,0,0,0,0,0,0,0,0,0};//频率记录 
	int i=0,j=0;//循环用变量 
    int max=0;//最高频率记录 
    
	scanf("%s",num);
	
	while(num[i] != '\0'){
		int temp=num[i]-'0';
		
		count[temp]++;
		if(max<count[temp]){
			max=count[temp];
		}//最高频率比较 
		len++;
		i++;
	}
	
	//从大到小依次匹配最大频率，并每匹配一轮讲最大频率-1直到最大频率为0 
	while(max!=0){    
	for(i=9;i>=0;i--){
		if(count[i] ==max){
			for(j=0;j<max;j++){
				printf("%d",i);
			} 
		} 
		
	}
	max--;
	}
    
	
	return 0;
} 
