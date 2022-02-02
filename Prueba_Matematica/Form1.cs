using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba_Matematica
{
    public partial class Form1 : Form
    {
        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();

        int addend1;
        int addend2;
        int minuend;
        int subtrahend;
        int multiplicand;
        int multiplier;
        int dividend;
        int divisor;
        int timeLeft;

        // Comenzar la prueba completando todos los problemas y poner en marcha el temporizador.

        public void StartTheQuiz()
        {
            // Genera dos números aleatorios para sumar. Almacenar los valores en las variables 'addend1' y 'addend2'.

            addend1 = randomizer.Next(51); //se hace para que se genere un numero aleatorio entre 0 y 50 
            addend2 = randomizer.Next(51); // entre los dos numeros daran una respuesta entre cero y cien

            //Convierte los dos números generados aleatoriamente en cadenas para que puedan mostrarse en los controles de etiqueta.

            plusLeftLabel.Text = addend1.ToString(); //es para que muestren los dos numeros aleatorios
            plusRightLabel.Text = addend2.ToString(); //en la etiqueta se muestra texto por eso con toString se convierte el numero en texto

            // 'Sum' es el nombre del control NumericUpDown. Este paso se asegura de que su valor sea cero antes de agregarle valores.
            Sum.Value = 0;

            //Fill in the subtraction problem. llenar en la sustracion del problema
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend); //el segundo valor del metodo Next() este elige un numero aleatorio que sea mayor o igual que el primer valor y menor que el segundo
                                                      // asi se asegura que la respuesta sea positiva
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0; //Este paso se asegura de que su valor sea cero antes de agregarle valores.

            // Fill in the multiplication problem. llenar en la multiplicacion del problema
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);    //la respuesta sera maximo 100
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem. llenar en la division del problema
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;         //la respuesta de la division no sera una fraccion
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            timeLeft = 30; //se establece la varible timeLeft en 30 
            timeLabel.Text = "30 segundos"; //muestra 30 segundos en el cuadro de time left
            timer1.Start(); //este metodo inicia la cuanta atras
        }

        private bool CheckTheAnswer() //determinar si las respuestas a los problemas matematicos son correctas
        {
            if ((addend1 + addend2 == Sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;    //compara los resultados con los valores de los controles NumericUpDown 
            else
                return false;   //si los valores son correctos debuelve true de lo contrario debuelve false           

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false; //el usuario no puede pulsar el boton durante una prueba
        }

        //Cada segundo de la prueba, se ejecuta este método. El código comprueba primero el valor que CheckTheAnswer() devuelve.
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer()) //si la respuesta es correcta el usuario recibe un mensaje y se para el tiempo
            {
                timer1.Stop();
                MessageBox.Show("FELICITACIONES!!, Tienes la repuesta correcta ");
                startButton.Enabled = true; //asi el usuario puede comenzar otra prueba
            }//si todas las respuestas son correctas ese valor es true y finaliza la prueba

            else if (timeLeft > 0) //se comprueba el valor de timeLeft y si es mayor a cero el temporizador resta 1 de timeLeft
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " segundos";//(muestra cuantos segundos quedan) actualiza la propiedad text del timeLabel
            }
            else
            {
                //si se acaba el tiempo se detiene el temporizador y cambia el texto a "se acabo el tiempo"
                timer1.Stop();
                timeLabel.Text = "se acabo el tiempo";
                MessageBox.Show("Fin de la prueba No terminaste a tiempo :("); //muestra este mensaje
                Sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;    //se revelan las respuestas correctas
                startButton.Enabled = true; //el usuario puede comenzar otra prueba (boton disponible)
            }
            if (timeLeft <= 5)
            {
                timeLabel.BackColor = Color.Red;
            }
            else if(timeLeft <= 30)
            {
                timeLabel.BackColor = Color.White;
            }

        }
        //se realiza este metodo para que cuando el usuario quiera escribir una respuesta se borre la que esta y pueda escribir una nueva

        private void answer_Enter(object sender, EventArgs e)
        //en este metodo el parametro sender hace referencia al objeto que tiene el evento que se genera (remitente -> NumericUpDown)
        {
            NumericUpDown answerBox = sender as NumericUpDown; //convierte el remitnte de un objeto generico a un control NumericUpDown y asigna el nombre de answeBox a este control
            //todos los controles NumericUpDown del formulario usarán este método, no solo el control del problema de suma.

            if (answerBox != null)//comprueba si se ha llevado a cabo correctamente la conversión de answerBox a un control NumericUpDown.
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;//determina la longitud de respuesta que esta en NumericUpDown
                answerBox.Select(0, lengthOfAnswer); //segun la longitud selecciona el valor actual en el control
            }

        }
    }
}
