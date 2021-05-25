using System.Collections.Generic;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Infra.Seeds
{
    public class CourseSeed
    {
        public static List<Course> Courses()
        {
            return new List<Course>()
            {
                new Course("Programação em Python do básico ao avançado",
                    "Aprenda Python 3.8.5 com Expressões Lambdas, Iteradores, Geradores, Orientação a Objetos e muito mais!",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971080/simple-mooc/courses/python-avan%C3%A7ado.png"),

                new Course("Curso de Python 3 do Básico Ao Avançado (com projetos reais)",
                    "Python 3 completo - programação com Django, PyQT5, Selenium, Regexp, Testes e TDD, POO, Design Patterns GoF, algoritmos",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971133/simple-mooc/courses/python-iniciante.jpg"),

                new Course("Java COMPLETO Programação Orientada a Objetos +Projetos",
                    "Curso mais didático e completo de Java e OO, UML, JDBC, JavaFX, Spring Boot, JPA, Hibernate, MySQL, MongoDB e muito mais",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971240/simple-mooc/courses/java.jpg"),

                new Course("Java 2021 COMPLETO: Do Zero ao Profissional + Projetos!",
                    "Fundamentos Java, Orientação a Objeto, Programação Funcional, MySQL, MongoDB, Spring Boot, JavaFX, JPA, Hibernate e mais",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971249/simple-mooc/courses/curso-java-devdojo.jpg"),

                new Course("REST API's RESTFul do 0 à Azure com ASP.NET Core 5 e Docker",
                    "Desenvolva uma API REST do zero absoluto atendendo todos os níveis de maturidade RESTful e implante na Azure + React JS",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971442/simple-mooc/courses/BannerCursosdotnet.png"),

                new Course("Angular, .NET Core Web API e Angular Material",
                    "Aprenda desenvolvendo um sistema em Angular, ASP.NET Core Web API e Angular Material",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971441/simple-mooc/courses/curso_o-que-e-net_1984.png"),

                new Course("Desenvolvimento de Aplicativos Android usando Kotlin",
                    "Adquira sólidos conhecimentos em Kotlin e utilize na criação de Apps Android! Curso mais completo sobre Kotlin em PT-BR!",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971703/simple-mooc/courses/Curso-completo-de-Kotlin-para-Android.jpg"),

                new Course("Automatizando Teste Mobile com o Espresso Android e Kotlin",
                    "Automatize testes nativos para Android com o framework Espresso!",
                    "https://res.cloudinary.com/dzcvxohec/image/upload/v1621971703/simple-mooc/courses/kotlin-curso.jpg")
            };
        }
    }
}