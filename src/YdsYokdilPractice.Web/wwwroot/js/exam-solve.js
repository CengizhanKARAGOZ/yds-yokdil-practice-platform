document.addEventListener("DOMContentLoaded", () => {
    const app = document.getElementById("examApp");
    const finishButton = document.getElementById("finishExamBtn");
    const resultBox = document.getElementById("resultBox");
    const showExplanationsButton = document.getElementById("showExplanationsBtn");

    if (!app || !finishButton) return;

    const examId = app.dataset.examId;
    const durationMinutes = parseInt(app.dataset.duration || "0", 10);

    let remainingSeconds = durationMinutes * 60;
    let timerInterval = null;
    let isFinished = false;

    const timerElement = document.getElementById("timer");

    function formatTime(seconds) {
        const min = Math.floor(seconds / 60);
        const sec = seconds % 60;

        return `${min.toString().padStart(2, "0")}:${sec.toString().padStart(2, "0")}`;
    }

    function startTimer() {
        if (!timerElement || remainingSeconds <= 0) return;

        timerElement.textContent = formatTime(remainingSeconds);

        timerInterval = setInterval(() => {
            remainingSeconds--;
            timerElement.textContent = formatTime(remainingSeconds);

            if (remainingSeconds <= 0) {
                clearInterval(timerInterval);
                finishExam();
            }
        }, 1000);
    }

    function markQuestionAsAnswered(questionId) {
        const navButton = document.querySelector(`.question-nav-btn[data-question-id="${questionId}"]`);

        if (navButton && !isFinished) {
            navButton.classList.remove("btn-outline-secondary");
            navButton.classList.add("btn-primary");
        }
    }

    document.querySelectorAll(".answer-option").forEach(input => {
        input.addEventListener("change", () => {
            markQuestionAsAnswered(input.dataset.questionId);
        });
    });

    function finishExam() {
        if (isFinished) return;

        isFinished = true;

        if (timerInterval) {
            clearInterval(timerInterval);
        }

        const questionCards = document.querySelectorAll(".question-card");

        let correct = 0;
        let wrong = 0;
        let blank = 0;

        const answers = [];

        questionCards.forEach(card => {
            const questionId = card.id.replace("question-", "");
            const selectedInput = card.querySelector(`input[name="question_${questionId}"]:checked`);
            const allInputs = card.querySelectorAll(`input[name="question_${questionId}"]`);
            const firstInput = card.querySelector(`input[name="question_${questionId}"]`);
            const correctAnswer = firstInput ? firstInput.dataset.correctAnswer : "";

            let selectedAnswer = null;
            let isCorrect = false;

            if (!selectedInput) {
                blank++;
                card.classList.add("border-secondary");

                const navButton = document.querySelector(`.question-nav-btn[data-question-id="${questionId}"]`);
                if (navButton) {
                    navButton.className = "btn btn-secondary btn-sm question-nav-btn";
                }
            } else {
                selectedAnswer = selectedInput.value;
                isCorrect = selectedAnswer === correctAnswer;

                if (isCorrect) {
                    correct++;
                    card.classList.add("border-success");

                    selectedInput.closest(".option-item").classList.add("list-group-item-success");

                    const navButton = document.querySelector(`.question-nav-btn[data-question-id="${questionId}"]`);
                    if (navButton) {
                        navButton.className = "btn btn-success btn-sm question-nav-btn";
                    }
                } else {
                    wrong++;
                    card.classList.add("border-danger");

                    selectedInput.closest(".option-item").classList.add("list-group-item-danger");

                    const correctInput = Array.from(allInputs).find(input => input.value === correctAnswer);
                    if (correctInput) {
                        correctInput.closest(".option-item").classList.add("list-group-item-success");
                    }

                    const navButton = document.querySelector(`.question-nav-btn[data-question-id="${questionId}"]`);
                    if (navButton) {
                        navButton.className = "btn btn-danger btn-sm question-nav-btn";
                    }
                }
            }

            allInputs.forEach(input => {
                input.disabled = true;
            });

            answers.push({
                questionId: Number(questionId),
                selectedAnswer,
                correctAnswer,
                isCorrect,
                isBlank: selectedAnswer === null
            });
        });

        const total = questionCards.length;
        const successRate = total > 0 ? Math.round((correct / total) * 100) : 0;

        document.getElementById("correctCount").textContent = correct;
        document.getElementById("wrongCount").textContent = wrong;
        document.getElementById("blankCount").textContent = blank;
        document.getElementById("successRate").textContent = `${successRate}%`;

        resultBox.classList.remove("d-none");
        finishButton.disabled = true;
        finishButton.textContent = "Deneme Tamamlandı";

        const result = {
            examId,
            solvedAt: new Date().toISOString(),
            correct,
            wrong,
            blank,
            total,
            successRate,
            answers
        };

        localStorage.setItem(`exam-result-${examId}`, JSON.stringify(result));

        resultBox.scrollIntoView({
            behavior: "smooth",
            block: "start"
        });
    }

    finishButton.addEventListener("click", finishExam);

    if (showExplanationsButton) {
        showExplanationsButton.addEventListener("click", () => {
            document.querySelectorAll(".explanation-box").forEach(box => {
                box.classList.remove("d-none");
            });

            showExplanationsButton.disabled = true;
            showExplanationsButton.textContent = "Açıklamalar Gösterildi";
        });
    }

    startTimer();
});