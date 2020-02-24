package project.domin;

public class Exam {
    private Integer exam_id;
    private Integer examination_id;
    private Integer exam_item;
    private String exam_date;
    private Integer order_number;
    private Integer max_number;

    public Exam() {
    }

    public Exam(Integer examination_id, Integer exam_item, String exam_date, Integer order_number, Integer max_number) {
        this.examination_id = examination_id;
        this.exam_item = exam_item;
        this.exam_date = exam_date;
        this.order_number = order_number;
        this.max_number = max_number;
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

    public Integer getExam_item() {
        return exam_item;
    }

    public void setExam_item(Integer exam_item) {
        this.exam_item = exam_item;
    }

    public String getExam_date() {
        return exam_date;
    }

    public void setExam_date(String exam_date) {
        this.exam_date = exam_date;
    }

    public Integer getOrder_number() {
        return order_number;
    }

    public void setOrder_number(Integer order_number) {
        this.order_number = order_number;
    }

    public Integer getMax_number() {
        return max_number;
    }

    public void setMax_number(Integer max_number) {
        this.max_number = max_number;
    }

    @Override
    public String toString() {
        return "Exam{" +
                "exam_id=" + exam_id +
                ", examination_id=" + examination_id +
                ", exam_item=" + exam_item +
                ", exam_date='" + exam_date + '\'' +
                ", order_number=" + order_number +
                ", max_number=" + max_number +
                '}';
    }
}
