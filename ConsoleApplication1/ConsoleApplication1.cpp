#include <SFML/Graphics.hpp>
#include <vector>
#include <sstream>

using namespace sf;

// Константы
const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
const int TOOLBAR_HEIGHT = 50;
const float MIN_BRUSH_RADIUS = 2.0f;
const float MAX_BRUSH_RADIUS = 20.0f;

// Структура кисти
struct Brush {
    Color color;
    float radius;
};

// Класс PaintApp для организации кода
class PaintApp {
private:
    RenderWindow window;
    RenderTexture canvas;
    Brush brush;
    Vector2f prevPosition;
    bool isDrawing = false;

    // Панель инструментов
    RectangleShape toolbar;
    std::vector<RectangleShape> colorButtons;
    std::vector<RectangleShape> sizeButtons;
    std::vector<Color> colors = { Color::Black, Color::Red, Color::Green, Color::Blue, Color::Yellow, Color::White };
    std::vector<float> sizes = { 2.0f, 5.0f, 10.0f, 15.0f, 20.0f }; // Предустановленные размеры кисти
    Font font; // Шрифт для текста
    std::vector<Text> sizeTexts; // Тексты для кнопок размера

public:
    PaintApp() : window(VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Simple Paint") {
        canvas.create(WINDOW_WIDTH, WINDOW_HEIGHT);
        canvas.clear(Color::White);

        // Инициализация кисти
        brush.color = Color::Black;
        brush.radius = 5.0f;

        // Загрузка шрифта
        if (!font.loadFromFile("arial.ttf")) { // Убедитесь, что файл шрифта доступен
            // Обработка ошибки
        }

        // Инициализация панели инструментов
        toolbar.setSize(Vector2f(WINDOW_WIDTH, TOOLBAR_HEIGHT));
        toolbar.setFillColor(Color(200, 200, 200));

        // Создание кнопок для выбора цвета
        for (size_t i = 0; i < colors.size(); ++i) {
            RectangleShape colorButton(Vector2f(40, 40));
            colorButton.setFillColor(colors[i]);
            colorButton.setPosition(10 + i * 50, 5);
            colorButtons.push_back(colorButton);
        }

        // Создание кнопок для выбора размера кисти
        for (size_t i = 0; i < sizes.size(); ++i) {
            RectangleShape sizeButton(Vector2f(40, 40));
            sizeButton.setFillColor(Color(150, 150, 150)); // Цвет кнопок размера
            sizeButton.setPosition(10 + (i + colors.size()) * 50, 5);
            sizeButtons.push_back(sizeButton);

            // Создание текста для кнопок размера
            Text sizeText;
            sizeText.setFont(font);
            sizeText.setString(std::to_string(i + 1)); // Цифры от 1 до 5
            sizeText.setCharacterSize(20); // Размер шрифта
            sizeText.setFillColor(Color::Black); // Цвет текста
            sizeText.setPosition(sizeButton.getPosition().x + 12, sizeButton.getPosition().y + 8); // Центрирование текста
            sizeTexts.push_back(sizeText);
        }
    }

    void run() {
        while (window.isOpen()) {
            handleEvents();
            draw();
        }
    }

private:
    void handleEvents() {
        Event event;
        while (window.pollEvent(event)) {
            if (event.type == Event::Closed)
                window.close();

            // Обработка выбора цвета
            if (event.type == Event::MouseButtonPressed && event.mouseButton.button == Mouse::Left) {
                Vector2i mousePos = Mouse::getPosition(window);
                if (mousePos.y < TOOLBAR_HEIGHT) { // Если клик в пределах панели инструментов
                    // Проверка кнопок цвета
                    for (size_t i = 0; i < colorButtons.size(); ++i) {
                        if (colorButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.color = colors[i];
                            return;
                        }
                    }

                    // Проверка кнопок размера
                    for (size_t i = 0; i < sizeButtons.size(); ++i) {
                        if (sizeButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.radius = sizes[i]; // Изменение размера кисти
                            return;
                        }
                    }
                }
            }
        }

        // Рисование линий
        if (Mouse::isButtonPressed(Mouse::Left) && Mouse::getPosition(window).y > TOOLBAR_HEIGHT) {
            Vector2f position = window.mapPixelToCoords(Mouse::getPosition(window));
            if (isDrawing) {
                drawCircle(canvas, prevPosition, brush);
            }
            prevPosition = position;
            isDrawing = true;
        }
        else {
            isDrawing = false;
        }
    }

    void draw() {
        canvas.display();
        Sprite sprite(canvas.getTexture());

        window.clear(Color::White);
        window.draw(sprite);
        drawToolbar();
        window.display();
    }

    void drawToolbar() {
        window.draw(toolbar);
        for (auto& colorButton : colorButtons) {
            window.draw(colorButton);
        }
        for (size_t i = 0; i < sizeButtons.size(); ++i) {
            window.draw(sizeButtons[i]);
            window.draw(sizeTexts[i]); // Отрисовка текста на кнопках размера
        }
    }

    // Функция для рисования круга (для кисти)
    void drawCircle(RenderTexture& canvas, Vector2f position, Brush brush) {
        CircleShape circle(brush.radius);
        circle.setFillColor(brush.color);
        circle.setPosition(position - Vector2f(brush.radius, brush.radius));
        canvas.draw(circle);
    }
};

int main() {
    PaintApp app;
    app.run();
    return 0;
}
