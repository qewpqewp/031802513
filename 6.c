#include<stdio.h>

int main(){
	char num[20]; //�������ִ�� 
	int len =0;//���ֳ��� 
	int count[10]={0,0,0,0,0,0,0,0,0,0};//Ƶ�ʼ�¼ 
	int i=0,j=0;//ѭ���ñ��� 
    int max=0;//���Ƶ�ʼ�¼ 
    
	scanf("%s",num);
	
	while(num[i] != '\0'){
		int temp=num[i]-'0';
		
		count[temp]++;
		if(max<count[temp]){
			max=count[temp];
		}//���Ƶ�ʱȽ� 
		len++;
		i++;
	}
	
	//�Ӵ�С����ƥ�����Ƶ�ʣ���ÿƥ��һ�ֽ����Ƶ��-1ֱ�����Ƶ��Ϊ0 
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
