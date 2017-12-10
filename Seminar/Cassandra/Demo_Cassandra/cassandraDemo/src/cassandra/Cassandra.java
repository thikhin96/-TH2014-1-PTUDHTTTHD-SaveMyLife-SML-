package cassandra;

import java.util.ArrayList;
import java.util.List;

import com.datastax.driver.core.Cluster;
import com.datastax.driver.core.PreparedStatement;
import com.datastax.driver.core.ProtocolVersion;
import com.datastax.driver.core.ResultSet;
import com.datastax.driver.core.Row;
import com.datastax.driver.core.Session;

import entities.student;

public class Cassandra {
	public static final Cassandra instance = new Cassandra();
	
	private final Cluster cluster;
	private final Session session;
	
	Cassandra() {
//		String[] hosts = PropertiesUtils.prop.getProperty("host", "127.0.0.1").split(",");
	
		cluster = Cluster.builder()
			    .addContactPoint("127.0.0.1")
			    .withProtocolVersion(ProtocolVersion.V3)
			    .build();
		Session session1 = cluster.connect();
		PreparedStatement stmt = session1.prepare("create keyspace if not exists cassandraDB with replication ={'class' : 'SimpleStrategy', 'replication_factor' : 1 }; ");
		session1.execute(stmt.bind());
		session = cluster.connect("cassandraDB");
//		stmt = session.prepare("create table if not exists student ( id int primary key, name text, phone text, address text );");
//		session.execute(stmt.bind());
	}
	
	public Cluster getCluster() {
		return cluster;
	}
	
	public int addStudent(int id, String name, String phone, String address) {
		PreparedStatement stmtAdd = session.prepare("insert into student (id, name, phone, address) values (?, ?, ?, ?)");
		return session.execute(stmtAdd.bind(id, name, phone, address)).wasApplied() ? 1 : 0;
	}
	
	public List<student> listStudent(){
		List<student> list = new ArrayList<student>();
		ResultSet rs = session.execute("select * from student");
		for(Row row : rs) {
			student s = new student(row.getInt(0), row.getString(1), row.getString(2), row.getString(3));
			list.add(s);
		}
		return list;
	}
}
