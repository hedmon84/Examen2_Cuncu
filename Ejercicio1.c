#include <stdio.h>
#include <stdlib.h>
#include <mpi.h>

/*

Generate exect : mpicc -g -Wall -o matriz_vector_pmi matriz_vector_pmi.c
run exect:mpiexec -n 4 ./matriz_vector_pmi 

*/

int **A;
int *x;
int *y;
int dimension = 8;
double data;
int size;
int mult;
int sum;
int my_rank;
void create_arrays();

void process(int my_rank, int size);

int main(void)
{
    // manda parametros del main
    MPI_Init(NULL, NULL);

    //retorna cantidad de procesos que estan ejecutandose
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    // ID del proceso que esta ejecutando esa linea
    MPI_Comm_rank(MPI_COMM_WORLD, &my_rank);

    process(my_rank, size);

    int result;

    result = mult + sum;

    printf(("final result %d", result));

    MPI_Finalize();

    return 0;
}

void create_arrays()
{
    printf("Creating array data\n");

    x = malloc(dimension * sizeof(int));

    for (int i = 0; i < dimension; i++)
    {

        x[i] = rand() % 50;
        printf("x[%d] = %d\n", i, x[i]);
    }

    printf("-----------------------------*-------------------------\n");
}

// void multiply()
// {
//     int dat_size = dimension / 4;
//     for (int i = 1; i < size; i++)
//     {

//         for (int j = 0; j < dat_size; j++)
//         {
//             data = x[j];
//         }
//         MPI_Send(&data, 1, MPI_DOUBLE, i, 0, MPI_COMM_WORLD);
//     }
// }

void process(int my_rank, int size)
{

    if (my_rank == 0)
    {

        create_arrays();

        int dat_size = dimension / 4;

        for (int i = 1; i < 4; i++)
        {
            data = 0;
            printf("for 1 %d\n", i);
            for (int j = 1; j < dat_size; j++)
            {

                printf("for 2 %d\n", j);
                data = x[j];

                MPI_Send(&data, 1, MPI_DOUBLE, i, 0, MPI_COMM_WORLD);
            }
        }
    }
    else
    {

        MPI_Recv(&data, 1, MPI_DOUBLE, 0, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);

        if (my_rank == 1)
        {

            if (data % 2 == 0)
            {
                mult *= data;
            }
            else
            {
                sum *= data;
            }

            printf("result thread = %d %lf\n", my_rank, data);
        }

        if (my_rank == 2)
        {
            if (data % 2 == 0)
            {
                mult *= data;
            }
            else
            {
                sum *= data;
            }

            printf("result thread = %d %lf\n", my_rank, data);
        }

        if (my_rank == 3)
        {

            if (data % 2 == 0)
            {
                mult *= data;
            }
            else
            {
                sum *= data;
            }

            printf("result thread = %d %lf\n", my_rank, data);
        }
    }
}
