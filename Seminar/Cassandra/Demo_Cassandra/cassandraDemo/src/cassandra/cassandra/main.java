package cassandra.cassandra;

import java.util.List;

import com.datastax.driver.core.Row;

import cassandra.Cassandra;
import entities.student;

public class main {
	public static void main(String[] agrs) {
//		if (Cassandra.instance.addStudent(1, "Thanh", "0123654789", "Ho CHi Minh") == 1)
//			System.out.println("ok");
//		else
//			System.out.println("fail");
		List<student> list = Cassandra.instance.listStudent();
		for(student s: list) {
			System.out.println(s.getId());
			System.out.println(s.getName());
			System.out.println(s.getAddress());
			System.out.println(s.getPhone());
			System.out.println();
		}
		
		System.exit(0);
	}
}
