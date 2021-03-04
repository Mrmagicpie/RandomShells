package xyz.mrmagicpie;
import java.util.Scanner;

public class Shell {
    private static void print(String input) {
        System.out.println(input);
    }

    public static void main(String[] args) {
        while(true) {
            Scanner input = new Scanner(System.in);
            System.out.print("Java Shell: $ ");
            String real_input = input.nextLine();
            print(real_input);
            input.close();
        }
    }
}
