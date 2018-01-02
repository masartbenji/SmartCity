package Model;

import java.util.Date;

public class Announcement {
    private int id;
    private Date date;
    private String Description;
    private double coordX;
    private double coordY;
    private Status status;
    private int idAnimal;

    public Announcement(int id, Date date, String description, double coordX, double coordY, Status status, int idAnimal) {
        this.id = id;
        this.date = date;
        Description = description;
        this.coordX = coordX;
        this.coordY = coordY;
        this.status = status;
        this.idAnimal = idAnimal;
    }

    public int getId() {
        return id;
    }
    public void setId(int id) {
        this.id = id;
    }

    public Date getDate() {
        return date;
    }
    public void setDate(Date date) {
        this.date = date;
    }

    public String getDescription() {
        return Description;
    }
    public void setDescription(String description) {
        Description = description;
    }

    public double getCoordX() {
        return coordX;
    }
    public void setCoordX(double coordX) {
        this.coordX = coordX;
    }

    public double getCoordY() {
        return coordY;
    }
    public void setCoordY(double coordY) {
        this.coordY = coordY;
    }



    public int getIdAnimal() {
        return idAnimal;
    }
    public void setIdAnimal(int idAnimal) {
        this.idAnimal = idAnimal;
    }

    public Status getStatus() {
        return status;
    }

    public void setStatus(Status status) {
        this.status = status;
    }
}
