package project.domin;

public class User {
    private Integer user_id;
    private String id_card;
    private String password;
    private String name;
    private String sex;
    private String phone;
    private Integer age;
    private Integer exam_status;

    public User() {
    }
    public User(Integer user_id, String id_card, String password, String name, String sex, String phone, Integer age, Integer exam_status) {
        this.user_id = user_id;
        this.id_card = id_card;
        this.password = password;
        this.name = name;
        this.sex = sex;
        this.phone = phone;
        this.age = age;
        this.exam_status = exam_status;
    }

    public Integer getUser_id() {
        return user_id;
    }

    public void setUser_id(Integer user_id) {
        this.user_id = user_id;
    }

    public String getId_card() {
        return id_card;
    }

    public void setId_card(String id_card) {
        this.id_card = id_card;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public Integer getAge() {
        return age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public Integer getExam_status() {
        return exam_status;
    }

    public void setExam_status(Integer exam_status) {
        this.exam_status = exam_status;
    }

    @Override
    public String toString() {
        return "User{" +
                "user_id=" + user_id +
                ", id_card='" + id_card + '\'' +
                ", password='" + password + '\'' +
                ", name='" + name + '\'' +
                ", sex='" + sex + '\'' +
                ", phone='" + phone + '\'' +
                ", age=" + age +
                ", exam_status=" + exam_status +
                '}';
    }
}
