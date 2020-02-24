package project.domin;

public class Order {
    Integer user_id;
    Integer exam_id;
    Integer examination_id;
    String exam_date;
    Integer grade;

    public Order() {
    }

    public Order(Integer user_id, Integer exam_id, Integer examination_id, String exam_date, Integer grade) {
        this.user_id = user_id;
        this.exam_id = exam_id;
        this.examination_id = examination_id;
        this.exam_date = exam_date;
        this.grade = grade;
    }

    public Integer getUser_id() {
        return user_id;
    }

    public void setUser_id(Integer user_id) {
        this.user_id = user_id;
    }

    public Integer getExam_id() {
        return exam_id;
    }

    public void setExam_id(Integer exam_id) {
        this.exam_id = exam_id;
    }

    public Integer getExamination_id() {
        return examination_id;
    }

    public void setExamination_id(Integer examination_id) {
        this.examination_id = examination_id;
    }

    public String getExam_date() {
        return exam_date;
    }

    public void setExam_date(String exam_date) {
        this.exam_date = exam_date;
    }

    public Integer getGrade() {
        return grade;
    }

    public void setGrade(Integer grade) {
        this.grade = grade;
    }

    @Override
    public String toString() {
        return "Order{" +
                "user_id=" + user_id +
                ", exam_id=" + exam_id +
                ", examination_id=" + examination_id +
                ", exam_date='" + exam_date + '\'' +
                ", grade=" + grade +
                '}';
    }
}
