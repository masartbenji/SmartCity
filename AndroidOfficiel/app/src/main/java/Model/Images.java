package Model;

public class Images {

    private int id;
    private String title;
    private Animal animal;


    public Images(int id, String title, Animal animal) {
        this.id = id;
        this.title = title;
        this.animal = animal;
    }

    public int getId() {
        return id;
    }
    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }
    public void setTitle(String title) {
        this.title = title;
    }

    public Animal getAnimal() {
        return animal;
    }
    public void setAnimal(Animal animal) {
        this.animal = animal;
    }
}
