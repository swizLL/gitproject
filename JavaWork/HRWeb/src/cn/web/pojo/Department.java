package cn.web.pojo;

public class Department {
    private int id;
    private int departmentnumber;
    private String name;
    private String manager;
    private String telephone;
    private String address;
    private String notes;

    public Department() {
    }
    public Department(int id, String manager, String telephone, String address, String notes) {
        this.id = id;
        this.manager = manager;
        this.telephone = telephone;
        this.address = address;
        this.notes = notes;
    }
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getDepartmentnumber() {
        return departmentnumber;
    }

    public void setDepartmentnumber(int departmentnumber) {
        this.departmentnumber = departmentnumber;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getManager() {
        return manager;
    }

    public void setManager(String manager) {
        this.manager = manager;
    }

    public String getTelephone() {
        return telephone;
    }

    public void setTelephone(String telephone) {
        this.telephone = telephone;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes;
    }
}
