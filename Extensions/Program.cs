namespace Extensions {
  public static class Enumerable {
    extension<T>(IEnumerable<T> source) {
        public void OlisExtra() {
          Console.WriteLine("Executing Oli's extra function");
        }
    }
  }

  class Program {
    static void Main(string[] args) {
      int[] numbers = [1, 2, 3, 4, 5];
      numbers.OlisExtra();
    }
  }
}
