public interface IMeow {
    public void meow(int amount = 1);

    public static void meowAll(IMeow[] meowables) {
        foreach(IMeow meowable in meowables)
            meowable.meow();
    }
}