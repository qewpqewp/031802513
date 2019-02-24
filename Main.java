import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
public class Main {

     static int getsketch(int hash, int mo){
        return Math.abs(hash % mo);
    }

    public static void main(String[] args) throws IOException {
         int Tmax=5; //视为黑客的最小值
        List<String> list = new ArrayList<String>();//储存黑客名单
        int[][] count =new int[9][10]; //多个count-min-sketch
        for (int i=0;i<=8;i++) {  //初始化
            for (int j=0;j<=9;j++
                 ) {count[i][j]=0;

            }

        }
        String m = "";
        BufferedReader file = new BufferedReader(new FileReader("Request.txt"));

        while ((m = file.readLine()) != null) {

            if (!m.equals("0"))
            {
                String [] arr = m.split(" ");
                for (int i=0;i<=8;i++) {

                    count[i][getsketch(arr[0].hashCode(),i+1)]++;
                    if ( list.contains(arr[0])==false){
                        int min=9999999;
                        for (int j=0;j<=8;j++) {
                            if(min> count[j][getsketch(arr[0].hashCode(),j+1)]){
                                min=count[j][getsketch(arr[0].hashCode(),j+1)];
                            }

                        }
                        if(min>=Tmax){
                        list.add(arr[0]);
                            System.out.println(arr[0]+"是黑客");
                        }


                }
            }
        }







    }
}
}
