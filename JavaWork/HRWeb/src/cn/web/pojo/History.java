package cn.web.pojo;

/**
 * 员工档案表
 * @author ZHT
 *
 */
public class History {

    private int id;
    private int employee_number;
    private String name;
    private String gender;
    private String birthday;
    private String telephone;
    private String email;
    private String address;
    private String photo;
    private String education;
    private int department_number;
    private int position_number;
    private String in_time;
    private String out_time;
    private String notes;
    private String status;
    private String home;



    public History() {
    }

    public History(int id, String birthday) {
        this.id = id;
        this.name = birthday;
    }
//    employeeNumber,name,gender,date,telephone,email,address,education,notes,home

    public History(int id, int employee_number, String name, String gender, String birthday, String telephone, String email, String address, String education, String notes, String home) {
        this.employee_number = employee_number;
        this.name = name;
        this.gender = gender;
        this.birthday = birthday;
        this.telephone = telephone;
        this.email = email;
        this.address = address;
        this.education = education;
        this.notes = notes;
        this.home = home;
        this.id = id;
    }

    public History(int id, String name, String gender, String birthday, String telephone, String email, String address, String education) {
        this.id = id;
        this.name = name;
        this.gender = gender;
        this.birthday = birthday;
        this.telephone = telephone;
        this.email = email;
        this.address = address;
        this.education = education;
    }

    public History(int employee_number, String name, String gender, String birthday, String telephone, String email, String address, String education, int department_number, int position_number) {
        this.employee_number = employee_number;
        this.name = name;
        this.gender = gender;
        this.birthday = birthday;
        this.telephone = telephone;
        this.email = email;
        this.address = address;
        this.education = education;
        this.department_number = department_number;
        this.position_number = position_number;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getEmployee_number() {
        return employee_number;
    }

    public void setEmployee_number(int employee_number) {
        this.employee_number = employee_number;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public String getTelephone() {
        return telephone;
    }

    public void setTelephone(String telephone) {
        this.telephone = telephone;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    public String getEducation() {
        return education;
    }

    public void setEducation(String education) {
        this.education = education;
    }

    public int getDepartment_number() {
        return department_number;
    }

    public void setDepartment_number(int department_number) {
        this.department_number = department_number;
    }

    public int getPosition_number() {
        return position_number;
    }

    public void setPosition_number(int position_number) {
        this.position_number = position_number;
    }

    public String getIn_time() {
        return in_time;
    }

    public void setIn_time(String in_time) {
        this.in_time = in_time;
    }

    public String getOut_time() {
        return out_time;
    }

    public void setOut_time(String out_time) {
        this.out_time = out_time;
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getHome() {
        return home;
    }

    public void setHome(String home) {
        this.home = home;
    }

    @Override
    public String toString() {
        return "History{" +
                "id=" + id +
                ", employee_number=" + employee_number +
                ", name='" + name + '\'' +
                ", gender='" + gender + '\'' +
                ", birthday='" + birthday + '\'' +
                ", telephone='" + telephone + '\'' +
                ", email='" + email + '\'' +
                ", address='" + address + '\'' +
                ", photo='" + photo + '\'' +
                ", education='" + education + '\'' +
                ", department_number=" + department_number +
                ", position_number=" + position_number +
                ", in_time='" + in_time + '\'' +
                ", out_time='" + out_time + '\'' +
                ", notes='" + notes + '\'' +
                ", status='" + status + '\'' +
                ", home='" + home + '\'' +
                "}\n";
    }
}

