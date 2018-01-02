package Model;

public class Animal {
    private int id;
    private String name;
    private Color color;
    private Breed breed;
    private Person person;


    public Animal(int id, String name, Color color, Breed breed, Person person) {
        this.id = id;
        this.name = name;
        this.color = color;
        this.breed = breed;
        this.person = person;
    }

    public int getId() {
        return id;
    }
    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }

    public Color getColor() {
        return color;
    }
    public void setColor(Color color) {
        this.color = color;
    }

    public Breed getBreed() {
        return breed;
    }
    public void setBreed(Breed breed) {
        this.breed = breed;
    }

    public Person getPerson() {
        return person;
    }
    public void setPerson(Person person) {
        this.person = person;
    }
}
