import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;
class MainTest {

    @Test
    void sum() {
        Main main = new Main();
        assertEquals(main.sum(3,4), 7);
    }
}