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
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161040/simple-mooc/course/python-avan%C3%A7ado_h13jug.png",
                    3),

                new Course("Curso de Python 3 do Básico Ao Avançado (com projetos reais)",
                    "Python 3 completo - programação com Django, PyQT5, Selenium, Regexp, Testes e TDD, POO, Design Patterns GoF, algoritmos",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161040/simple-mooc/course/python-iniciante_vb5ej6.png",
                    3),

                new Course("Java COMPLETO Programação Orientada a Objetos +Projetos",
                    "Curso mais didático e completo de Java e OO, UML, JDBC, JavaFX, Spring Boot, JPA, Hibernate, MySQL, MongoDB e muito mais",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161040/simple-mooc/course/java_hh9gib.png", 4),

                new Course("Java 2021 COMPLETO: Do Zero ao Profissional + Projetos!",
                    "Fundamentos Java, Orientação a Objeto, Programação Funcional, MySQL, MongoDB, Spring Boot, JavaFX, JPA, Hibernate e mais",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161039/simple-mooc/course/curso-java-devdojo_r80y1w.png",
                    5),

                new Course("REST API's RESTFul do 0 à Azure com ASP.NET Core 5 e Docker",
                    "Desenvolva uma API REST do zero absoluto atendendo todos os níveis de maturidade RESTful e implante na Azure + React JS",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161039/simple-mooc/course/BannerCursosdotnet_abeiyn.png"),

                new Course("Angular, .NET Core Web API e Angular Material",
                    "Aprenda desenvolvendo um sistema em Angular, ASP.NET Core Web API e Angular Material",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161038/simple-mooc/course/curso_o-que-e-net_1984_anfkrq.png",
                    4),

                new Course("Desenvolvimento de Aplicativos Android usando Kotlin",
                    "Adquira sólidos conhecimentos em Kotlin e utilize na criação de Apps Android! Curso mais completo sobre Kotlin em PT-BR!",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161039/simple-mooc/course/Curso-completo-de-Kotlin-para-Android_kvhkid.png",
                    5),

                new Course("Automatizando Teste Mobile com o Espresso Android e Kotlin",
                    "Automatize testes nativos para Android com o framework Espresso!",
                    "https://res.cloudinary.com/simplemooc/image/upload/v1623161039/simple-mooc/course/kotlin-curso_patvqo.png",
                    4)
            };
        }
    }
}