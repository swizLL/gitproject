package project.domin;

public class Examination {
    private Integer examination_id;
    private String address;
    private String examination_name;

    public Examination() {
    }

    public Examination(String address, String examination_name) {
        this.address = address;
        this.examination_name = examination_name;
    }

    public Examination(Integer examination_id, String address, String examination_name) {
        this.examination_id = examination_id;
        this.address = address;
        this.examination_name = examination_name;
    }

    public Integer getExamination_id() {
        return examination_id;
    }

    public void setExamination_id(Integer examination_id) {
        this.examination_id = examination_id;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getExamination_name() {
        return examination_name;
    }

    public void setExamination_name(String examination_name) {
        this.examination_name = examination_name;
    }

    @Override
    public String toString() {
        return "ExamNation{" +
                "examination_id=" + examination_id +
                ", address='" + address + '\'' +
                ", examination_name='" + examination_name + '\'' +
                '}';
    }
}
